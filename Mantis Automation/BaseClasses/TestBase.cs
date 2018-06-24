using Mantis_Automation.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis_Automation.BaseClasses
{
   /* public enum BrowserType
    {
        Chrome,
        Firefox,
        IE,
        Opera
    }

    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Firefox)]
    [TestFixture(BrowserType.Opera)]
    [TestFixture(BrowserType.IE)]
    [Parallelizable(ParallelScope.Fixtures)]*/
    public class TestBase
    {

        public IWebDriver driver { get; set; }

        [SetUp]
        public void SetupTest()
        {
            driver = new DriverFactory().Initialize(ConfigurationManager.AppSettings["Browser"]);
        }

        [TearDown]
        public void TearDownTest()
        {

            if (NUnit.Framework.TestContext.CurrentContext.Result.FailCount > 0)
            {
                string nomeArquivo = DriverFactory.TakeScreenshotOnException(driver, NUnit.Framework.TestContext.CurrentContext.Test.MethodName);
                NUnit.Framework.TestContext.AddTestAttachment(System.AppDomain.CurrentDomain.BaseDirectory + "../../Resources/Screenshots/" + nomeArquivo, "evidência da tela onde o erro ocorreu!");
            }
            driver.Quit();

        }
    }
}
