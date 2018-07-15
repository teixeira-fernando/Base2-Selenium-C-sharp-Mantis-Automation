using Mantis_Automation.BaseClasses;
using Mantis_Automation.Helpers;
using Mantis_Automation.Steps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Collections;

namespace Mantis_Automation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class Test_Login : TestBase
    {

        #region Objetos de página e steps
        LoginSteps loginSteps = null;
        MyViewSteps myViewSteps = null;
        #endregion

        #region Data driven provider
        public static IEnumerable loginDataProvider(int linha)
        {
            return DataDrivenCSV.retornaDadosCSV(System.AppDomain.CurrentDomain.BaseDirectory
                + "../../Resources/TestData/Login.csv", linha);
        }

        public static IEnumerable loginDataProvider_Conjunto(int[] linha)
        {

            return DataDrivenCSV.retornaDadosCSV(System.AppDomain.CurrentDomain.BaseDirectory
                + "../../Resources/TestData/Login.csv", linha);
        }

        #endregion

        [TestMethod, TestCaseSource("loginDataProvider", new object[] { 1 })]
        public void SuccessLogin(ArrayList dadosTeste)
        {
            #region Instância de steps
            loginSteps = new LoginSteps(driver);
            myViewSteps = new MyViewSteps(driver);
            #endregion

            loginSteps.doLogin(dadosTeste[0].ToString(), dadosTeste[1].ToString());
            NUnit.Framework.Assert.IsTrue(myViewSteps.myViewPage.Is_LoggedInUserLabel_Visible());
        }

        [TestMethod, TestCaseSource("loginDataProvider_Conjunto", new object[] { new int[] { 2, 3, 4 } })]
        public void FailLogin(ArrayList dadosTeste)
        {
            /*Case 1: Wrong password
             * Case 2: Wrong username
             * Case 3: Wrong password and username             
             */

            #region Instância de steps
            loginSteps = new LoginSteps(driver);
            myViewSteps = new MyViewSteps(driver);
            #endregion

            loginSteps.doLogin(dadosTeste[0].ToString(), dadosTeste[1].ToString());
            NUnit.Framework.Assert.IsFalse(myViewSteps.myViewPage.Is_LoggedInUserLabel_Visible());

        }

    }
}
