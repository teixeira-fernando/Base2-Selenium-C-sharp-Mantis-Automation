using Mantis_Automation.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
    public class ReportIssuePage
    {
        #region Variáveis e Construtor
        WebDriverWait wait = null;
        int waitComponent;
        IWebDriver driver = null;
        #endregion

        public ReportIssuePage(IWebDriver driverReference)
        {
            driver = driverReference;
            waitComponent = Convert.ToInt16(ConfigurationManager.AppSettings["ComponentTimeout"]);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitComponent));
        }

        #region Web Elements

        #region Select Project

        protected IWebElement ChooseProjectComboBox => driver.FindElement(By.Name("project_id"));
        protected IWebElement SelectProjectButton => driver.FindElement(By.ClassName("button"));
        protected IWebElement FormTitleLabel => driver.FindElement(By.ClassName("form-title"));

        #endregion

        #region Report Details

        protected IWebElement CategoryComboBox => driver.FindElement(By.Name("category_id"));
        protected IWebElement SummaryTextField => driver.FindElement(By.Name("summary"));
        protected IWebElement DescriptionTextField => driver.FindElement(By.Name("description"));
        protected IWebElement SubmitReportButton => driver.FindElement(By.CssSelector("input[value='Submit Report']"));

        #endregion

        #region Report Result

        protected IWebElement ReportIssueResult => driver.FindElement(By.XPath("/html/body/div[2]"));

        #endregion

        #endregion

        #region Methods

        public bool verify_ReportIssueResultMessage(string message)
        {
            wait.Until(WaitHelper.URLToBeDiffent("http://mantis-prova.base2.com.br/bug_report_page.php"));
            return ReportIssueResult.Text.Contains(message);
        }

        public bool isFormTitleVisible()
        {
            return FormTitleLabel.Displayed;
        }

        public void selectProject(string project)
        {
            wait.Until(WaitHelper.ElementIsVisible(ChooseProjectComboBox));
            var selectElement = new SelectElement(ChooseProjectComboBox);
            selectElement.SelectByText(project);
        }

        public void clickSelectProjectButton()
        {
            wait.Until(WaitHelper.ElementIsVisible(SelectProjectButton));
            SelectProjectButton.Click();
        }

        public void selectCategory(string category)
        {
            wait.Until(WaitHelper.ElementIsVisible(CategoryComboBox));
            var selectElement = new SelectElement(CategoryComboBox);
            selectElement.SelectByText(category);
        }

        public void fillSummaryField(string summary)
        {
            wait.Until(WaitHelper.ElementIsVisible(SummaryTextField));
            SummaryTextField.SendKeys(summary);
        }

        public void fillDescriptionField(string description)
        {
            wait.Until(WaitHelper.ElementIsVisible(DescriptionTextField));
            DescriptionTextField.SendKeys(description);
        }

        public void clickSubmitReportButton()
        {
            wait.Until(WaitHelper.ElementIsVisible(SubmitReportButton));
            Actions actions = new Actions(driver);
            actions.MoveToElement(SubmitReportButton);
            actions.Perform();
            SubmitReportButton.Click();
        }

        #endregion
    }
}
