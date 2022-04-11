using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Webmotors.WrapperFactory;

namespace Webmotors.Tests
{
    public class BaseTests : BrowserFactory
    {
 

        public BaseTests()
        {
            BasicConfigurator.Configure();
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
       

        protected void iniciarTestesSemLogar()
        {
            QuitChrome();

            BrowserFactory.InitBrowser("chrome");
            BrowserFactory.LoadApplication(Global.URL);
        }
       

        [TestCleanup]
        public void tearDown()
        {
            BrowserFactory.Driver.Quit();
        }

        public void QuitChrome()
        {
            string[] LstProcess = { "chrome", "chromedriver" };
            for (int i = 0; i < LstProcess.Length; i++)
            {
                string NomeProcess = LstProcess[i];
                foreach (Process Proc in Process.GetProcessesByName(NomeProcess))
                {
                    try
                    {
                        Proc.Kill();
                    }
                    catch { }
                }
            }
        }
    }
}
