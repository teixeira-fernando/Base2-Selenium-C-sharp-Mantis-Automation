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
            waitComponent = Convert.ToInt16(ConfigurationManager.AppSettings["ComponentTimeout"]);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitComponent));
        }

        #region Web Elements

        protected IWebElement UserNameField => driver.FindElement(By.Name("username"));

        protected IWebElement PasswordField => driver.FindElement(By.Name("password"));

        protected IWebElement LoginButton => driver.FindElement(By.ClassName("button"));


        #endregion

        #region Methods

        public void fillUsernameField(string username)
        {
            wait.Until(WaitHelper.ElementIsVisible(UserNameField));
            UserNameField.SendKeys(username);
        }

        public void fillPasswordField(string password)
        {
            wait.Until(WaitHelper.ElementIsVisible(UserNameField));
            PasswordField.SendKeys(password);
        }

        public void clickOnLoginButton()
        {
            wait.Until(WaitHelper.ElementIsVisible(LoginButton));
            LoginButton.Click();
        }

        #endregion
    }
}
