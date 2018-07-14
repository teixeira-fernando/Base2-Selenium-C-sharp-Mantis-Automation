using Mantis_Automation.BaseClasses;
using Mantis_Automation.Helpers;
using Mantis_Automation.Steps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis_Automation.Tests
{
    [TestClass]
    public class Test_ReportIssue : TestBase
    {
        #region Objetos de página e steps
        LoginSteps loginSteps = null;
        MyViewSteps myViewSteps = null;
        ReportIssueSteps reportIssueSteps = null;
        ViewIssuesSteps viewIssuesSteps = null;
        #endregion

        #region Data driven provider
        public static IEnumerable testReportDataProvider(int linha)
        {
            return DataDrivenCSV.retornaDadosCSV(System.AppDomain.CurrentDomain.BaseDirectory
                + "../../Resources/TestData/TestReport.csv", linha);
        }

        public static IEnumerable testReportProvider_Conjunto(int[] linha)
        {

            return DataDrivenCSV.retornaDadosCSV(System.AppDomain.CurrentDomain.BaseDirectory
                + "../../Resources/TestData/TestReport.csv", linha);
        }

        #endregion

        [Parallelizable]
        [TestMethod, TestCaseSource("testReportProvider_Conjunto", new object[] { new int[] { 1 } } )]
        public void TestReport_BasicReport(ArrayList dadosTeste)
        {
            #region Instância de steps
            loginSteps = new LoginSteps(driver);
            myViewSteps = new MyViewSteps(driver);
            reportIssueSteps = new ReportIssueSteps(driver);
            #endregion

            #region Pre-conditions
            loginSteps.doLogin(dadosTeste[0].ToString(), dadosTeste[1].ToString());
            myViewSteps.myViewPage.ClickOnReportIssueLink();
            #endregion

            reportIssueSteps.createNewReport(dadosTeste[2].ToString(), dadosTeste[3].ToString(), dadosTeste[4].ToString(), dadosTeste[5].ToString());

            NUnit.Framework.Assert.IsTrue(reportIssueSteps.reportIssue.verify_ReportIssueResultMessage("Operation successful."));
        }
    }
}
