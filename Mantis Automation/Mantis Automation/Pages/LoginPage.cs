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
    public class LoginPage
    {
        #region Variáveis e Construtor
        WebDriverWait wait = null;
        int waitComponent;
        IWebDriver driver = null;
        #endregion

        public LoginPage()
        {
            PageFactory.InitElements(DriverFactory.Instance, this);
            waitComponent = Convert.ToInt16(ConfigurationManager.AppSettings["ComponentTimeout"]);
            wait = new WebDriverWait(DriverFactory.Instance, TimeSpan.FromSeconds(waitComponent));
            driver = DriverFactory.Instance;
        }

        #region Web Elements

        [FindsBy(How = How.Name, Using = "username")]
        protected IWebElement UserNameField { get; set; }

        [FindsBy(How = How.Name, Using = "password")]
        protected IWebElement TitleMessageToUser { get; set; }

        [FindsBy(How = How.Id, Using = "new_task")]
        protected IWebElement TaskNameField { get; set; }


        #endregion

    }
}
