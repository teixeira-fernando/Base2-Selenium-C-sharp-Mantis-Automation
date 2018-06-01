using Mantis_Automation.BaseClasses;
using Mantis_Automation.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Collections;

namespace Mantis_Automation.Tests
{
    [TestClass]
    public class Test_Login : TestBase
    {
        #region Objetos de página e steps
        //PesquisaMoedasCadastradasSteps pesquisaMoedasCadastradasSteps = null;
       // DBSteps dbSteps = null;
        #endregion

        #region Data driven provider
        public static IEnumerable loginDataProvider(int linha)
        {
            return DataDrivenCSV.retornaDadosCSV(System.AppDomain.CurrentDomain.BaseDirectory
                + "../../Resources/TestData/Login.csv", linha);
        }
        #endregion

        [TestMethod, TestCaseSource("loginDataProvider", new object[] { 1 })]
        public void testLogin(ArrayList dadosTeste)
        {
         /*   #region Instância de paginas e steps
            pesquisaMoedasCadastradasSteps = new PesquisaMoedasCadastradasSteps();
            dbSteps = new DBSteps();
            #endregion

            #region Dados do teste
            string codigoMoeda = dbSteps.retornaMoedasCadastrada(dadosTeste[0].ToString());
            string status = dadosTeste[0].ToString();
            #endregion

            pesquisaMoedasCadastradasSteps.abrirPagina();
            pesquisaMoedasCadastradasSteps.acessarMenuMoedas();
            pesquisaMoedasCadastradasSteps.selecionarFiltroMoeda(codigoMoeda);
            pesquisaMoedasCadastradasSteps.selecionarFiltroStatus(status);
            pesquisaMoedasCadastradasSteps.acionarBotaoPesquisar();
            string descricaoMoedaFiltro = pesquisaMoedasCadastradasSteps.lerValorSelecionadoFiltroMoeda();
            string descricaoMoedaGrid = pesquisaMoedasCadastradasSteps.lerDescricaoMoedaGrid(descricaoMoedaFiltro);

            //NUnit.Framework.Assert.AreEqual(descricaoMoedaFiltro, descricaoMoedaGrid);
            */
        }
    }
}
