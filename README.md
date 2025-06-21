# MathCaptchaBatchDownloader

Render **15** math‐CAPTCHAs at a time in an ASP.NET MVC‑5 app,  
download them as a ZIP (filenames are `unknown_<token>.png`),  
auto‑refresh the page, and repeat until you’ve harvested the number of
batches you want (default = 100).

---

## 📑 Table of Contents
1. [Project goals](#project-goals)  
2. [Folder structure](#folder-structure)  
3. [Prerequisites](#prerequisites)  
4. [Quick start](#quick-start)  
5. [How it works](#how-it-works)  
6. [Configuration](#configuration)  
7. [Troubleshooting](#troubleshooting)  
8. [License](#license)

---

## Project goals
* **Pixel‑perfect capture** – copies rendered pixels from the
  browser’s memory, so you never get 1 × 1 blanks.
* **Hands‑free bulk download** – one click starts a loop that produces
  *N* ZIPs (100 by default) with automatic page reloads between cycles.
* **Anonymous filenames** – every PNG is
  `unknown_<guid>.png`, where `<guid>` is the `t=` token from the captcha
  URL.
* **No backend answers** – filenames intentionally use `unknown_` prefix—
  no reflection into CaptchaMvc internals.

---

## 🗂 Folder structure
```
MathCaptchaBatchDownloader/
│  MathCaptchaBatch.csproj
│  Global.asax
│  Global.asax.cs
│  Web.config
│
├─App_Start/
│     RouteConfig.cs
│
├─Controllers/
│     HomeController.cs
│     (BulkCaptchaController.cs)*
│
└─Views/
    ├─Web.config
    └─Home/
         BulkCaptchas.cshtml
         BatchPartial.cshtml
```
\* server‑side ZIP maker kept for reference.

---

## Prerequisites
| Tool | Why |
|------|-----|
| **.NET SDK 8.x** | builds the SDK‑style csproj (targets net48) |
| **Visual Studio 2022 / Build Tools 2022** | optional – gives IIS Express |
| **IIS Express 10** | simplest local host |

---

## 🚀 Quick start
```powershell
git clone https://github.com/<your‑account>/MathCaptchaBatchDownloader.git
cd MathCaptchaBatchDownloader

dotnet restore
dotnet build -c Release

iisexpress /path:%CD% /port:5000
start http://localhost:5000/
```
Click **Download 100×**; page reloads automatically until 100 ZIPs save.

---

## ⚙️ Configuration
| What | Where | Default |
|------|-------|---------|
| CAPTCHAs per page | `BulkCaptchas.cshtml` | 15 |
| ZIPs per click | `const TOTAL_ZIPS` | 100 |
| Filename prefix | same script | `"unknown"` |
| Browser prompt | browser settings | disable “Ask where to save each file” |

---

## 🛠 Troubleshooting
| Issue | Fix |
|-------|-----|
| Empty zip | check browser console; verify images decoded |
| Global.asax type error | ensure DLLs in */bin* |
| Prompt appears each download | turn off browser download‑prompt |

---

## 📜 License
MIT
