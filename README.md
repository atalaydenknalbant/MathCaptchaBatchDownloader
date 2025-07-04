# MathCaptchaBatchDownloader

> 🌐 **Hosted demo**:  
> https://my-captcha-downloader-a2btbmdvhsg3eked.polandcentral-01.azurewebsites.net/  
> ⚠️ To use the hosted site you must:
> 1. **Allow automatic downloads** for this domain in your browser settings.  
> 2. **Disable** the option **"Ask where to save each file before downloading"** so ZIPs download seamlessly.

Render **15** (maximum before blanks appear) math‑CAPTCHAs at a time in an ASP.NET MVC 5 app,  
download them as a ZIP (filenames are `unknown_<token>.png`),  
auto‑refresh the page, and repeat until you’ve harvested the number of  
batches you want (default = 2000).

---

## 📑 Table of Contents
1. [Project goals](#project-goals)  
2. [Folder structure](#folder-structure)  
3. [Prerequisites](#prerequisites)  
4. [Quick start](#quick-start)  
5. [Hosted site](#hosted-site)  
6. [Configuration](#configuration)  
7. [Troubleshooting](#troubleshooting)  
8. [License](#license)

---

## Project goals

* **Pixel‑perfect capture** – copies rendered pixels from the
  browser’s memory, so you never get blank captchas.  
* **Hands‑free bulk download** – one click starts a loop that produces
  *N* ZIPs (2000 by default) with automatic page reloads between cycles.  
* **Anonymous filenames** – every PNG is
  `unknown_<guid>.png`, where `<guid>` is the `t=` token from the captcha
  URL.  
* **Powered by CaptchaMvc** – uses the NuGet package  
  **[CaptchaMvc.Mvc5 1.5.0](https://www.nuget.org/packages/CaptchaMvc.Mvc5)**  
  to generate math CAPTCHAs server‑side.

---

## 🗂 Folder structure

```
MathCaptchaBatchDownloader/
│  MathCaptchaBatchDownloader.csproj
│  Global.asax
│  Global.asax.cs
│  Web.config
│
├─App_Start/
│     RouteConfig.cs
│
├─Controllers/
│     HomeController.cs
│     BulkCaptchaController.cs
│
└─Views/
    ├─Web.config
    └─Home/
         BulkCaptchas.cshtml
         BatchPartial.cshtml
```

---

## Prerequisites
| Tool                                       | Why                                                  |
|--------------------------------------------|------------------------------------------------------|
| **.NET SDK 8.x**                           | builds the SDK‑style csproj (targets net48)          |
| **Visual Studio 2022 / Build Tools 2022**  | optional – gives IIS Express                         |
| **IIS Express 10**                         | simplest local host                                  |

---

## 🚀Quick start 

```powershell
git clone https://github.com/atalaydenknalbant/MathCaptchaBatchDownloader.git
cd MathCaptchaBatchDownloader

dotnet restore
dotnet build -c Release

iisexpress /path:%CD% /port:5000
start http://localhost:5000/Home/BulkCaptchas
```
Click **Download 100×**; page reloads automatically until 2000 ZIPs save.

---

## 🌐Hosted site

You can also use the publicly hosted version here:
```
https://my-captcha-downloader-a2btbmdvhsg3eked.polandcentral-01.azurewebsites.net/
```

> **Note:** ensure your browser settings:
> - **Allow automatic downloads** for this domain.  
> - **Disable** “Ask where to save each file before downloading.”

---

## ⚙Configuration
| What                         | Where                        | Default    |
|------------------------------|------------------------------|------------|
| CAPTCHAs per page            | `BulkCaptchas.cshtml`        | 15         |
| ZIPs per click               | `const TOTAL_ZIPS` (JS)      | 2000       |
| Filename prefix              | same script (JS)             | `"unknown"`|
| Browser “save as” prompt     | browser settings             | disable it |

---

## 🛠Troubleshooting
| Issue                         | Fix                                                       |
|-------------------------------|-----------------------------------------------------------|
| Empty zip                     | check browser console; verify images decoded              |
| Global.asax type error        | ensure DLLs are in the `/bin` folder                     |
| Download prompt appears       | turn off “Ask where to save each file” in browser settings|

---

## 📜License
This project is licensed under the MIT License – see the [LICENSE](LICENSE) file for details.
