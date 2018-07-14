using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Events;
using System;
using System.Configuration;

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
                    ChromeOptions options = new ChromeOptions();
                    capability.SetCapability(ChromeOptions.Capability, options);
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

        
    }
}
