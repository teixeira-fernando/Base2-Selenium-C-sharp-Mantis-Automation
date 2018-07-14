using Mantis_Automation.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis_Automation.Steps
{
    public class ViewIssuesSteps
    {
        public ViewIssuesPage viewIssues;

        public ViewIssuesSteps(IWebDriver driverReference)
        {
            viewIssues = new ViewIssuesPage(driverReference);
        }
    }
}
