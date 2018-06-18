using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.PhantomJS;

namespace Mantis_Automation.Helpers
{
    public class DriverFactory
    {
        public static IWebDriver Instance { get; set; }
        /// <summary>
        /// Base URL of the site being tested.
        /// </summary>
        public static string BaseUrl { get; set; }

        static DriverFactory()
        {
            Instance = null;
        }

        public static void Initialize(string browser)
        {
            // If we are running against a remote webdriver.
            if (ConfigurationManager.AppSettings["RemoteChromeDriver"].Equals("true"))
            {

                if (browser.Equals("Firefox"))
                {
                    DesiredCapabilities capability = new DesiredCapabilities();

                    FirefoxOptions options = new FirefoxOptions();
                    options.BrowserExecutableLocation = "C:\\Program Files(x86)\\Mozilla Firefox\\firefox.exe";
                    options.SetPreference("marionette", false);

                    capability.SetCapability("name", "NUnit-CBT");
                    capability.SetCapability("record_video", "true");
                    capability.SetCapability("build", "1.0");
                    capability.SetCapability("platform", "Windows 10");
                    capability.SetCapability(FirefoxDriver.BinaryCapabilityName, "C:\\Program Files(x86)\\Mozilla Firefox\\firefox.exe");
                    capability.SetCapability("browserName", "Firefox");
                    Instance = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capability);
                    
                }
                else if (browser.Equals("Chrome"))
                {
                    Instance = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), DesiredCapabilities.Chrome());
                }
                else if (browser.Equals("InternetExplorer"))
                {
                    Instance = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), DesiredCapabilities.InternetExplorer());
                }
                else if (browser.Equals("PhamtomJS"))
                {
                    Instance = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), DesiredCapabilities.PhantomJS());
                }
            }
            else
            {
                if (browser.Equals("Firefox"))
                {
                    //Create the service driver
                    var driverService = FirefoxDriverService.CreateDefaultService();
                    driverService.FirefoxBinaryPath = "C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe";
                    driverService.HideCommandPromptWindow = true;

                    Instance = new FirefoxDriver(driverService);
                    Instance.Manage().Window.Maximize();
                    
                }
                else if (browser.Equals("Chrome"))
                {
                    // Create the driver.
                    var options = new ChromeOptions();
                    options.AddArgument("--start-maximized");

                    // Create an event firing webdriver.
                    var firingDriver = new EventFiringWebDriver(new ChromeDriver(System.AppDomain.CurrentDomain.BaseDirectory, options, TimeSpan.FromSeconds(180)));

                    Instance = firingDriver;                  
                }
                else if (browser.Equals("Opera"))
                {
                    OperaOptions options = new OperaOptions();
                    options.BinaryLocation = "C:\\Program Files\\Opera\\launcher.exe";

                    Instance = new OperaDriver(options);
                }
                else if (browser.Equals("InternetExplorer"))
                {
                    var options = new InternetExplorerOptions()
                    {
                       // InitialBrowserUrl = ConfigurationManager.AppSettings["BaseUrl"],
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        IgnoreZoomLevel = true,
                        EnableNativeEvents = false,
                        EnablePersistentHover = false
                    };
                    Instance = new InternetExplorerDriver(options);
                }
                else if (browser.Equals("PhamtomJS"))
                {                  
                    Instance = new PhantomJSDriver();
                }
            }

            // Initialize base URL
            BaseUrl = ConfigurationManager.AppSettings["BaseURL"];

            Instance.Navigate().GoToUrl(BaseUrl);

            double implicitlyWait = Convert.ToDouble(ConfigurationManager.AppSettings["DefaultImplicitlyWait"]);
            Instance.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(implicitlyWait));

            double pageLoadTimeout = Convert.ToDouble(ConfigurationManager.AppSettings["PageLoadTimeout"]);
            Instance.Manage().Timeouts().PageLoad = (TimeSpan.FromSeconds(pageLoadTimeout));
        }

        private static FirefoxProfile CreateFirefoxProfile()
        {
            var firefoxProfile = new FirefoxProfile();
            //firefoxProfile.SetPreference("network.automatic-ntlm-auth.trusted-uris", "http://localhost");
            return firefoxProfile;
        }

        /// <summary>
        /// Take a screen shot everytime Selenium has an exception
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Exception.</param>
        private static void TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
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
                Instance.TakeScreenshot().SaveAsFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\Screenshots_Errors\" + e.ThrownException.StackTrace[0].ToString() + "   " + timestamp + ".png");
            }
        }

        public static string TakeScreenshotOnException(string methodName)
        {

            using (var ms = new MemoryStream())
            {

                /*string searchTerm = String.Format("{0}*.png", "");
                var files = Directory.EnumerateFiles(System.AppDomain.CurrentDomain.BaseDirectory + @"\Screenshots_Errors\", searchTerm);

                // Delete files.
                foreach (string fileName in files)
                {
                    try
                    {
                        if (fileName.Contains(methodName))
                        {
                            File.Delete(fileName);
                        }
                    }
                    catch (IOException)
                    {
                        // Wait a bit then try again.
                        Thread.Sleep(1000);
                        File.Delete(fileName);
                    }
                }*/

                // Create a new one with the latest exception.
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");

                Instance.TakeScreenshot().SaveAsFile(System.AppDomain.CurrentDomain.BaseDirectory + "../../Resources/Screenshots/" + methodName + "-" + timestamp + ".png");
                return methodName + "-" + timestamp + ".png";
            }
        }
    }
}
