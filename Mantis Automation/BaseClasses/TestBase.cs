﻿using Mantis_Automation.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Configuration;

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

    //[WatchDog(SaveInClass.AllTests)]
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
                string nomeArquivo = TakeScreenshotsHelper.TakeScreenshotOnException(driver, NUnit.Framework.TestContext.CurrentContext.Test.MethodName);
                NUnit.Framework.TestContext.AddTestAttachment(System.AppDomain.CurrentDomain.BaseDirectory + "Screenshots/" + nomeArquivo, "evidência da tela onde o erro ocorreu!");
            }
            driver.Quit();
        }
    }
}
