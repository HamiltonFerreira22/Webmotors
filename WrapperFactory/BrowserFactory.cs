using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Webmotors.WrapperFactory
{
    public class BrowserFactory
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver driver;
        private static WebDriverWait wait;
        public static int WaitElementTo = 60;
        public static bool osLinux = false;

        public static IWebDriver Driver
        {
            get
            {
                return driver;
            }
            private set
            {
                driver = value;
            }
        }

        public static WebDriverWait Wait
        {
            get
            {
                return wait;
            }
        }

        public static void InitBrowser(string browserName, bool pheadless = false, bool pChromeIncognito = true)
        {
            var os = Environment.OSVersion;
            osLinux = os.Platform == PlatformID.Unix;
            string projectPath = @System.Environment.CurrentDirectory.ToString();

            switch (browserName)
            {
                case "OPERA":
                    string operaDriverPath;
                    OperaOptions optionsOpera = new OperaOptions();
                    OperaDriverService serviceOpera;

                    if (osLinux)
                    {
                        operaDriverPath = "/usr/bin/";
                        serviceOpera = OperaDriverService.CreateDefaultService(operaDriverPath, "operadriver");
                    }
                    else
                    {
                        operaDriverPath = projectPath;
                        serviceOpera = OperaDriverService.CreateDefaultService(operaDriverPath, "operadriver.exe");
                    }

                    driver = new OperaDriver();
                    wait = new WebDriverWait(driver, new TimeSpan(0, 0, WaitElementTo));
                    Driver.Manage().Window.Maximize();
                    break;


                case "EDGE":
                    var optionsEdge = new EdgeOptions();
                    EdgeDriverService serviceEdge;

                    string edgeDriverPath;
                    optionsEdge.AddArgument("-inprivate");

                    optionsEdge.AcceptInsecureCertificates = true;

                    if (osLinux)
                    {
                        edgeDriverPath = "/usr/bin/";
                        serviceEdge = EdgeDriverService.CreateDefaultService(edgeDriverPath, "msedgedriver");
                    }
                    else
                    {
                        edgeDriverPath = projectPath;
                        serviceEdge = EdgeDriverService.CreateDefaultService(edgeDriverPath, "msedgedriver.exe");
                    }

                    driver = new EdgeDriver(serviceEdge, optionsEdge, TimeSpan.FromMinutes(1));
                    wait = new WebDriverWait(driver, new TimeSpan(0, 0, WaitElementTo));
                    Driver.Manage().Window.Maximize();
                    break;

                case "chrome":

                    var options = new ChromeOptions();
                    options.AddArgument("--no-sandbox");
                    if (pheadless)
                    {
                        options.AddArgument("--headless");
                    }
                    if (pChromeIncognito)
                    {
                        options.AddArgument("--incognito");
                    }
                    options.AddArgument("--window-size=1366,768");

                    driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(1));
                    if (Environment.OSVersion.Platform == PlatformID.Unix)
                    {
                        options.BinaryLocation = "/usr/bin/google-chrome";

                    }
                    else
                    {
                        if (File.Exists("C://Program Files (x86)//Google//Chrome//Application//chrome.exe"))
                        {
                            options.BinaryLocation = "C://Program Files (x86)//Google//Chrome//Application//chrome.exe";
                        }
                        else if (File.Exists("C:/Program Files/Google/Chrome/Application/chrome.exe"))
                        {
                            options.BinaryLocation = "C:/Program Files/Google/Chrome/Application/chrome.exe";
                        }

                    }
                    wait = new WebDriverWait(driver, new TimeSpan(0, 0, WaitElementTo));
                    Driver.Manage().Window.Maximize();
                    if (Driver == null)
                        Drivers.Add("chrome", Driver);
                    break;

            }
        }

        public class Global
        {
        public static string URL = "https://hportal.webmotors.com.br/";
        public static string NomeMetodo = "";

        }

        public void ScreenShot(string testContext)
        {
            var projectPath = Path.GetFullPath(@"TestResults").Remove(Path.GetFullPath(@"TestResults").IndexOf("bin"));
            var path = projectPath + $"TestResults{Path.DirectorySeparatorChar}Print{Path.DirectorySeparatorChar}{testContext}";

            if (!Directory.Exists(path))
            {
                _ = Directory.CreateDirectory(path);
            }
            string fileName = $"{path}{Path.DirectorySeparatorChar}{System.DateTime.Now:yyyy-MM-dd-HH-mm-ss}.jpeg";
            OpenQA.Selenium.Screenshot screenShot = ((OpenQA.Selenium.ITakesScreenshot)BrowserFactory.Driver).GetScreenshot();
            screenShot.SaveAsFile(fileName);
        }


        public static void LoadApplication(string url)
        {
            Driver.Url = url;
        }

        public static void CloseDriver()
        {
            Driver.Close();
            Driver.Dispose();
            Driver = null;
            Drivers.Remove("chrome");
        }
        public static void AssertAreEqual(string texto, By elemento)
        {
            Assert.AreEqual(texto, BrowserFactory.Driver.FindElement(elemento).Text.ToString());
        }

    }
}
