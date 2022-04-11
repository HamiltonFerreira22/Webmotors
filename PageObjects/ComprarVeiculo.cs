using log4net;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Webmotors.Extencions;
using Webmotors.WrapperFactory;


namespace Webmotors.PageObjects
{
    public class ComprarVeiculo : BrowserFactory
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Elementos
        [FindsBy(How = How.XPath, Using = "//*[@id='WhiteBox']/div[1]/div[1]/h1")]//Campo Comprar carros
        private IWebElement CampoComprarCarros { get; set; }
        [FindsBy(How = How.XPath, Using = "//*[@id='searchBar']")]//Campo Digitar marca ou modelo
        private IWebElement CampoMarcaModelo { get; set; }

        public void ComprarCarro()
        {            
            BrowserFactory.Driver.EsperarElemento(By.XPath("//*[@id='logoHomeWebmotors']/img"));
            BrowserFactory.Driver.FindElement(By.XPath("//button[./text()='OK']")).Clicar(1000);
            BrowserFactory.Driver.FindElement(By.Id("//*[@id='searchBar'']")).PreencherTexto("Honda");
            BrowserFactory.Driver.FindElement(By.XPath("//div/strong[./text()='Honda ']"));
            if (BrowserFactory.Driver.VerificarElementoPresente(By.XPath("//div[contains(text(),'Não encontramos este termo, verifique a ortografia')]")))
            {
               
                BrowserFactory.Driver.Esperar(1000);
                BrowserFactory.Driver.FindElement(By.XPath("//*[@id='searchBar']")).PreencherTexto("Honda");
                BrowserFactory.Driver.Esperar(1000);
            }
            BrowserFactory.Driver.FindElement(By.XPath("//div/strong[./text()='Honda ']")).Clicar(1000);


            if (BrowserFactory.Driver.VerificarElementoPresente(By.XPath("//h2[contains(text(),'HONDA CITY')]")))
            {
                BrowserFactory.Driver.FindElement(By.XPath("//h2[contains(text(),'HONDA CITY')]")).Clicar();
            }
            else
            {
                BrowserFactory.Driver.FindElement(By.XPath("//h2[contains(text(),'HONDA FIT')]")).Clicar();
            }


            BrowserFactory.Driver.EsperarElementoFicarVisivel(By.XPath("//span[contains(text(),' Simule seu financiamento sem compromisso! ')]"));
            BrowserFactory.Driver.FindElement(By.XPath("//*[@id='VehicleBasicInformation']/div/div[1]/div[1]"));
            BrowserFactory.Driver.pressKey("Down");
        }

        public void ComprarMoto()
        {
            BrowserFactory.Driver.EsperarElemento(By.XPath("//*[@id='logoHomeWebmotors']/img"));
            BrowserFactory.Driver.FindElement(By.XPath("//button[./text()='OK']")).Clicar(1000);
            //Escolha do modal motos
            BrowserFactory.Driver.FindElement(By.XPath("//h2/a[contains(text(),'Comprar Motos')]")).Clicar(100);
            BrowserFactory.Driver.Esperar(1000);
            BrowserFactory.Driver.FindElement(By.XPath("//*[@id='searchBar']")).PreencherTexto("Honda");
            BrowserFactory.Driver.Esperar(1000);
            if (BrowserFactory.Driver.VerificarElementoPresente(By.XPath("//div[contains(text(),'Não encontramos este termo, verifique a ortografia')]")))
            {
                BrowserFactory.Driver.FindElement(By.XPath("//h2/a[contains(text(),'Comprar Motos')]")).Clicar(100);
                BrowserFactory.Driver.Esperar(1000);
                BrowserFactory.Driver.FindElement(By.XPath("//*[@id='searchBar']")).PreencherTexto("Honda");
                BrowserFactory.Driver.Esperar(1000);
            }
            BrowserFactory.Driver.FindElement(By.XPath("//div/strong[./text()='Honda ']")).Clicar(1000);


            if (BrowserFactory.Driver.VerificarElementoPresente(By.XPath("//h2[contains(text(),'HONDA CB 300R')]")))
            {
                BrowserFactory.Driver.FindElement(By.XPath("//h2[contains(text(),'HONDA CB 300R')]")).Clicar(1000);
            }
            else
            {
                BrowserFactory.Driver.FindElement(By.XPath("//h2[contains(text(),'HONDA XRE 300')]")).Clicar();
            }
            BrowserFactory.Driver.EsperarElementoFicarVisivel(By.XPath("//span[contains(text(),' Simule seu financiamento sem compromisso! ')]"));
            BrowserFactory.Driver.FindElement(By.XPath("//*[@id='VehicleBasicInformation']/div/div[1]/div[1]"));
            BrowserFactory.Driver.pressKey("Down");
        }

    }  
}
