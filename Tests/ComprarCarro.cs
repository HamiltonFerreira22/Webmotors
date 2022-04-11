using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.DevTools;
using log4net;
using log4net.Config;
using Webmotors.Tests;
using Webmotors.PageObjects;
using System;

namespace Webmotors
{
    [TestClass]
    public class ComprarCarro : BaseTests
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [TestInitialize]
        public void SetUp()
        {
            BasicConfigurator.Configure();
            iniciarTestesSemLogar();
        }

        [TestMethod]
        public void Comprar()
        {
            log.Info("Iniciando Teste: Comprar carro!");
            Page.ComprarVeiculo.ComprarCarro();
        }

    }
}
