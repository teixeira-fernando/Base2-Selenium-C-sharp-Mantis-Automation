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
using System.IO;
using System.Threading;
using OpenQA.Selenium.PhantomJS;

namespace Mantis_Automation.Helpers
{
    public class DriverFactory
    {
        public IWebDriver Instance { get; set; }
        /// <summary>
        /// Base URL of the site being tested.
        /// </summary>
        public static string BaseUrl { get; set; }

        public IWebDriver Initialize(string browser)
        {           
            // If we are running against a remote webdriver.
            if (ConfigurationManager.AppSettings["RemoteChromeDriver"].Equals("true"))
            {
                DesiredCapabilities capability = new DesiredCapabilities();
                capability.SetCapability("platform", "WINDOWS");
                if (browser.Equals("Firefox"))
                {
                    capability = DesiredCapabilities.Firefox();
                }
                else if (browser.Equals("Chrome"))
                {
                    capability = DesiredCapabilities.Chrome();
                }
                else if (browser.Equals("InternetExplorer"))
                {
                    capability = DesiredCapabilities.InternetExplorer();
                }
                else if (browser.Equals("PhamtomJS"))
                {
                    capability = DesiredCapabilities.PhantomJS();
                }
                Instance = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capability);
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

            return Instance;
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

                driver.TakeScreenshot().SaveAsFile(System.AppDomain.CurrentDomain.BaseDirectory + "../../Resources/Screenshots/" + methodName + "-" + timestamp + ".png");
                return methodName + "-" + timestamp + ".png";
            }
        }
    }
}
