using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis_Automation.Pages
{
    public class ViewIssuesPage
    {
        #region Variáveis e Construtor
        WebDriverWait wait = null;
        int waitComponent;
        IWebDriver driver = null;
        #endregion

        public ViewIssuesPage(IWebDriver driverReference)
        {
            driver = driverReference;
            waitComponent = Convert.ToInt16(ConfigurationManager.AppSettings["ComponentTimeout"]);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitComponent));
        }

        #region Web Elements

        protected IWebElement BugListTable => driver.FindElement(By.Id("buglist"));

        #endregion

        #region Methods

        public bool Is_BugListTable_Visible()
        {
            try
            {
                return BugListTable.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        #endregion
    }
}
