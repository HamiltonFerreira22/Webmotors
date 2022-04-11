using log4net;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Webmotors.PageObjects;

namespace Webmotors.Tests
{
    [TestClass]
    public class ComprarMoto : BaseTests
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
            log.Info("Iniciando Teste: Comprar moto!");
            Page.ComprarVeiculo.ComprarMoto();
        }

    }
}
