using Mantis_Automation.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis_Automation.Steps
{
    public class MyViewSteps
    {
        public MyViewPage myViewPage;

        public MyViewSteps(IWebDriver driverReference)
        {
            myViewPage = new MyViewPage(driverReference);
        }



    }
}
