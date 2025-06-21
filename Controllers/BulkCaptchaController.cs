using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.Infrastructure;   // CaptchaUtils, DefaultCaptchaManager

namespace MathCaptchaBatch.Controllers
{
    public class BulkCaptchaController : Controller
    {
        // GET /BulkCaptcha/Zip
        public async Task<ActionResult> Zip()
        {
            /* 0 ─ workspace */
            string workDir = Server.MapPath("~/App_Data/Captchas");
            Directory.CreateDirectory(workDir);

            /* 1 ─ HttpClient with shared ASP.NET_SessionId */
            var cookies = new CookieContainer();
            var handler = new HttpClientHandler { CookieContainer = cookies, UseCookies = true };
            using var http = new HttpClient(handler)
            {
                BaseAddress = new Uri($"{Request.Url.Scheme}://{Request.Url.Authority}/")
            };

            /* 2 ─ fetch 15 helpers */
            string html = await http.GetStringAsync("Home/BatchPartial?count=15");
            System.IO.File.WriteAllText(Path.Combine(workDir, "debug.html"), html);

            /* 3 ─ find every /DefaultCaptcha/Generate… */
            var rx = new Regex(
                @"<img\b[^>]+?(?:(?:src|data-url)\s*=\s*['""])(?<u>/DefaultCaptcha/Generate[^'""]+)",
                RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

            var mgr = (DefaultCaptchaManager)CaptchaUtils.CaptchaManager;

            foreach (Match m in rx.Matches(html))
            {
                string imgUrl = WebUtility.HtmlDecode(m.Value);

                /* token id=… (or legacy c=…) */
                var q = HttpUtility.ParseQueryString(new Uri(http.BaseAddress, imgUrl).Query);
                string token = q["id"] ?? q["c"];            // v1.5 is id=, older samples used c=
                if (string.IsNullOrEmpty(token)) continue; 

                /* download bitmap */
                http.DefaultRequestHeaders.Referrer =
                    new Uri(http.BaseAddress, "Home/BatchPartial");
                byte[] png = await http.GetByteArrayAsync(imgUrl);

                /* answer via internal dictionaries */
                string answer = "unknown";
                var store     = mgr.StorageProvider;
                Type t        = store.GetType();
                IDictionary draw = (IDictionary)t.GetProperty("DrawingKeys",
                               BindingFlags.NonPublic | BindingFlags.Instance).GetValue(store, null);
                IDictionary val  = (IDictionary)t.GetProperty("ValidateKeys",
                               BindingFlags.NonPublic | BindingFlags.Instance).GetValue(store, null);

                object cap = val[token] ?? draw[token];
                if (cap != null)
                {
                    var vProp = cap.GetType().GetProperty("Value");
                    if (vProp?.GetValue(cap) is object vObj)
                        answer = vObj.ToString();
                }

                string hex = Guid.NewGuid().ToString("N").Substring(0, 10);
                string fn  = $"{answer}_{hex}.png";
                System.IO.File.WriteAllBytes(Path.Combine(workDir, fn), png);
            }

            /* 4 ─ zip & stream */
            string zipPath = Server.MapPath("~/App_Data/captchas.zip");
            if (System.IO.File.Exists(zipPath)) System.IO.File.Delete(zipPath);
            ZipFile.CreateFromDirectory(workDir, zipPath);

            return File(zipPath, "application/zip", "captchas.zip");
        }
    }
}
