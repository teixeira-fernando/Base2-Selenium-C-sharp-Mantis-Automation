using Mantis_Automation.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis_Automation.Pages
{
    public class MyViewPage
    {
        #region Variáveis e Construtor
        WebDriverWait wait = null;
        int waitComponent;
        IWebDriver driver = null;
        #endregion

        public MyViewPage(IWebDriver driverReference)
        {
            driver = driverReference;
            waitComponent = Convert.ToInt16(ConfigurationManager.AppSettings["ComponentTimeout"]);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitComponent));
        }

        #region Web Elements

        protected IWebElement LoggedInUserLabel => driver.FindElement(By.ClassName("login-info-left"));

        protected IWebElement ReportIssueLink => driver.FindElement(By.LinkText("Report Issue"));

        #endregion

        #region Methods

        public bool Is_LoggedInUserLabel_Visible()
        {
            try
            {
                return LoggedInUserLabel.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ClickOnReportIssueLink()
        {
            wait.Until(WaitHelper.ElementIsVisible(ReportIssueLink));
            ReportIssueLink.Click();
        }

        #endregion
    }
}
