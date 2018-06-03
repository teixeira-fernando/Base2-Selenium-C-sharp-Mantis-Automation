using Mantis_Automation.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis_Automation.BaseClasses
{
    public class TestBase
    {

        [SetUp]
        public void SetupTest()
        {
            DriverFactory.Initialize(ConfigurationManager.AppSettings["Browser"]);
        }

        [TearDown]
        public void TearDownTest()
        {

            if (NUnit.Framework.TestContext.CurrentContext.Result.FailCount > 0)
            {
                string nomeArquivo = DriverFactory.TakeScreenshotOnException(NUnit.Framework.TestContext.CurrentContext.Test.MethodName);
                NUnit.Framework.TestContext.AddTestAttachment(System.AppDomain.CurrentDomain.BaseDirectory + "../../Resources/Screenshots/" + nomeArquivo, "evidência da tela onde o erro ocorreu!");
            }
            DriverFactory.Instance.Quit();

        }
    }
}
