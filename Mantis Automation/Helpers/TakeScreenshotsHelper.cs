using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mantis_Automation.Helpers
{
    public class TakeScreenshotsHelper
    {
        private static void TakeScreenshotOnException(IWebDriver driver, object sender, WebDriverExceptionEventArgs e)
        {
            // Find all existing  screenshot files for this module.
            using (var ms = new MemoryStream())
            {

                string searchTerm = String.Format("{0}*.png", "");
                var files = Directory.EnumerateFiles(System.AppDomain.CurrentDomain.BaseDirectory + @"\Screenshots_Errors\", searchTerm);

                // Delete files.
                foreach (string fileName in files)
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch (IOException)
                    {
                        // Wait a bit then try again.
                        Thread.Sleep(1000);
                        File.Delete(fileName);
                    }
                }

                // Create a new one with the latest exception.
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
                driver.TakeScreenshot().SaveAsFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\Screenshots_Errors\" + e.ThrownException.StackTrace[0].ToString() + "   " + timestamp + ".png");
            }
        }

        public static string TakeScreenshotOnException(IWebDriver driver, string methodName)
        {

            using (var ms = new MemoryStream())
            {
                // Create a new one with the latest exception.
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "/Screenshots");
                
                driver.TakeScreenshot().SaveAsFile(System.AppDomain.CurrentDomain.BaseDirectory + "/Screenshots/" + methodName + "-" + timestamp + ".png");
                return methodName + "-" + timestamp + ".png";
            }
        }
    }
}
