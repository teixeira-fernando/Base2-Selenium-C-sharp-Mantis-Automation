using Mantis_Automation.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis_Automation.Steps
{
    public class LoginSteps
    {
        private LoginPage loginPage;

        public LoginSteps(IWebDriver driverReference)
        {
            loginPage = new LoginPage(driverReference);
        }

        public void doLogin(string username, string password)
        {
            loginPage.fillUsernameField(username);
            loginPage.fillPasswordField(password);
            loginPage.clickOnLoginButton();
        }
    }
}
