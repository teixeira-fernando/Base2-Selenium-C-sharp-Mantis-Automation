using Mantis_Automation.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis_Automation.Steps
{
    public class ReportIssueSteps
    {
        private ReportIssuePage reportIssue;

        public ReportIssueSteps(IWebDriver driverReference)
        {
            reportIssue = new ReportIssuePage(driverReference);
        }

        public void createNewReport(string project, string category, string summary)
        {
            if (reportIssue.isFormTitleVisible()) { 
                reportIssue.selectProject(project);
                reportIssue.clickSelectProjectButton();
            }
            reportIssue.selectCategory(category);
            reportIssue.setSummaryValue(summary);
            reportIssue.clickSubmitReportButton();
        }
    }
}
