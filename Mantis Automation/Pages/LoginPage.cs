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

        public LoginPage(IWebDriver driverReference)
        {
            driver = driverReference;
            PageFactory.InitElements(driver, this);
            waitComponent = Convert.ToInt16(ConfigurationManager.AppSettings["ComponentTimeout"]);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitComponent));
        }

        #region Web Elements

        [FindsBy(How = How.Name, Using = "username")]
        protected IWebElement UserNameField { get; set; }

        [FindsBy(How = How.Name, Using = "password")]
        protected IWebElement PasswordField { get; set; }

        [FindsBy(How = How.ClassName, Using = "button")]
        protected IWebElement LoginButton { get; set; }


        #endregion

        #region Methods

        public void fillUsernameField(string username)
        {
            UserNameField.SendKeys(username);
        }

        public void fillPasswordField(string password)
        {
            PasswordField.SendKeys(password);
        }

        public void clickOnLoginButton()
        {
            LoginButton.Click();
        }

        #endregion
    }
}
