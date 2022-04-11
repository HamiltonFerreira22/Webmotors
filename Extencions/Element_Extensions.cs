using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Webmotors.WrapperFactory;

namespace Webmotors.Extencions
{
    public static class Element_Extensions
    {
        private static IWebElement pageFullLoaded = null;

        public static void PreencherTexto(this IWebElement element, string text)
        {
            interagirElemento(element);
            element.Clear();
            element.SendKeys(text);
        }

        public static void PreencherTexto(this IWebElement element, string text, int miliseconds)
        {
            BrowserFactory.Driver.Esperar(miliseconds);
            interagirElemento(element);
            element.Clear();
            element.SendKeys(text);
        }

        public static bool IsDisplayed(this IWebElement element)
        {
            bool result;
            try
            {
                result = element.Displayed;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public static void Clicar(this IWebElement element, int miliseconds = 0)
        {

            interagirElemento(element);
            element.Click();
            BrowserFactory.Driver.Esperar(miliseconds);
        }

        public static void SelectByText(this IWebElement element, string text, int miliseconds = 0)
        {
            BrowserFactory.Driver.Esperar(miliseconds);
            interagirElemento(element);
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(text);
        }

        public static void SelectByIndex(this IWebElement element, int index, int miliseconds = 0)
        {
            interagirElemento(element);
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByIndex(index);
        }

        public static void SelectByValue(this IWebElement element, string text, int miliseconds = 0)
        {
            BrowserFactory.Driver.Esperar(miliseconds);
            interagirElemento(element);
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByValue(text);
        }

        public static void Esperar(this IWebDriver driver, int miliseconds)
        {
            Thread.Sleep(miliseconds);
        }

        public static void ConfirmarAlerta(this IWebDriver driver, bool value, int miliseconds = 0)
        {
            BrowserFactory.Driver.Esperar(miliseconds);
            IAlert alert = driver.SwitchTo().Alert();
            if (value)
                alert.Accept();
            else
                alert.Dismiss();
        }

        public static string PegarTextoAlerta(this IWebDriver driver)
        {
            string textoAlerta = driver.SwitchTo().Alert().Text;

            return textoAlerta;
        }

        public static void EsperarElemento(this IWebDriver driver, By findElement)
        {
            pageFullLoaded = null;
            waitLoadPage(pageFullLoaded);
            try
            {
                BrowserFactory.Wait.Until(ExpectedConditions.ElementExists(findElement));
            }
            catch (WebDriverException)
            {
                esperarCarregamentoAjax(driver);
                pageFullLoaded = null;
                waitLoadPage(pageFullLoaded);
                BrowserFactory.Wait.Until(ExpectedConditions.ElementExists(findElement));
            }

        }
        public static void EsperarElementoCotacao(this IWebDriver driver, By findElement)
        {
            pageFullLoaded = null;
            waitLoadPage(pageFullLoaded);
            try
            {
                BrowserFactory.Wait.Until(ExpectedConditions.ElementExists(findElement));
            }
            catch (WebDriverException)
            {
                esperarCarregamentoAjax(driver);
                pageFullLoaded = null;
                waitLoadPage(pageFullLoaded);
                BrowserFactory.Wait.Until(ExpectedConditions.ElementExists(findElement));
            }

        }

        public static void EsperarOcultarElemento(this IWebDriver driver, By findElement)
        {
            pageFullLoaded = null;
            waitLoadPage(pageFullLoaded);
            BrowserFactory.Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(findElement));
        }

        public static void EsperarElementoLocado(this IWebDriver driver, By findElement)
        {
            pageFullLoaded = null;
            waitLoadPage(pageFullLoaded);
            BrowserFactory.Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(findElement));
        }

        public static void EsperarElementoFicarVisivel(this IWebDriver driver, By findElement)
        {
            pageFullLoaded = null;
            waitLoadPage(pageFullLoaded);
            BrowserFactory.Wait.Until(ExpectedConditions.ElementIsVisible((findElement)));
        }

        public static bool VerificarElementoPresente(this IWebDriver driver, By findElement, int waitSeconds = 0)
        {
            int i = 0;
            bool elementExists = false;
            while (!elementExists && i <= waitSeconds)
            {
                BrowserFactory.Driver.Esperar(1000);
                try
                {
                    BrowserFactory.Driver.FindElement(findElement);
                    elementExists = true;
                }
                catch (NoSuchElementException)
                {
                    elementExists = false;
                }

                i++;
            }
            return elementExists;
        }

        public static bool VerificarElementoExibido(this IWebDriver driver, By findElement, int waitSeconds = 0)
        {
            int i = 0;
            bool elementDisplayed = false;
            while (!elementDisplayed && i <= waitSeconds)
            {
                BrowserFactory.Driver.Esperar(1000);
                if (VerificarElementoPresente(driver, findElement))
                {
                    if (BrowserFactory.Driver.FindElement(findElement).IsDisplayed())
                        elementDisplayed = true;
                }
                else
                    elementDisplayed = false;
                i++;
            }
            return elementDisplayed;
        }

        public static string getElementText(this IWebDriver driver, By findElement)
        {
            interagirElemento(BrowserFactory.Driver.FindElement(findElement));
            return BrowserFactory.Driver.FindElement(findElement).Text;
        }

        public static void EsperarElementoImplicitamente(this IWebDriver driver, By findElement, int waitSeconds)
        {
            int i = 0;
            bool elementExists = false;
            List<IWebElement> elements = new List<IWebElement>();
            while (!elementExists && i < waitSeconds)
            {
                BrowserFactory.Driver.Esperar(1000);
                elementExists = BrowserFactory.Driver.FindElements(findElement).Count() != 0;
                i++;
            }
        }

        public static void EsperarOcultarElementoImplicitamente(this IWebDriver driver, By findElement, int segundosEspera)
        {
            int i = 0;
            bool elementExists = true;
            List<IWebElement> elements = new List<IWebElement>();
            while (elementExists && i < segundosEspera)
            {
                BrowserFactory.Driver.Esperar(1000);
                elementExists = BrowserFactory.Driver.FindElements(findElement).Count() != 0;
                i++;
            }
        }

        public static bool VerificarAlertaExistente(this IWebDriver driver)
        {
            IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
            return (alert != null);
        }

        public static bool VerificarAlertaExistente(this IWebDriver driver, int waitSeconds)
        {
            int i = 0;
            IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
            while (i < waitSeconds && alert == null)
            {
                BrowserFactory.Driver.Esperar(1000);
                alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
                i++;
            }

            return (alert != null);
        }

        public static void pressKey(this IWebDriver driver, string key)
        {
            Actions builder = new Actions(driver);
            switch (key)
            {
                case "Tab":
                    builder.SendKeys(OpenQA.Selenium.Keys.Tab).Perform();
                    break;
                case "Up":
                    builder.SendKeys(OpenQA.Selenium.Keys.Up).Perform();
                    break;
                case "Down":
                    builder.SendKeys(OpenQA.Selenium.Keys.Down).Perform();
                    break;
                case "Enter":
                    builder.SendKeys(OpenQA.Selenium.Keys.Enter).Perform();
                    break;
                case "Home":
                    builder.SendKeys(OpenQA.Selenium.Keys.Home).Perform();
                    break;
                case "SetaParaBaixo":
                    builder.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();
                    break;

            }
        }

        public static void mudarParaNovaAba(this IWebDriver driver)
        {
            var newTabInstance = BrowserFactory.Driver.WindowHandles[BrowserFactory.Driver.WindowHandles.Count - 1];

            BrowserFactory.Driver.SwitchTo().Window(newTabInstance);
        }

        public static void fecharNovaAba(this IWebDriver driver)
        {
            var originalTabInstance = BrowserFactory.Driver.WindowHandles[BrowserFactory.Driver.WindowHandles.Count - 2];

            var newTabInstance = BrowserFactory.Driver.WindowHandles[BrowserFactory.Driver.WindowHandles.Count - 1];

            BrowserFactory.Driver.SwitchTo().Window(newTabInstance);
            BrowserFactory.Driver.Close();

            BrowserFactory.Driver.SwitchTo().Window(originalTabInstance);
            BrowserFactory.Driver.SwitchTo().DefaultContent();
        }

        //public static void EnviarArquivo(this IWebDriver driver, string caminhoArquivo)
        //{
        //    BrowserFactory.Driver.Esperar(500);
        //    SendKeys.SendWait("^a");
        //    BrowserFactory.Driver.Esperar(500);
        //    SendKeys.SendWait(caminhoArquivo);
        //    BrowserFactory.Driver.Esperar(500);
        //    SendKeys.SendWait(@"{Enter}");
        //}

        public static void esperarCarregamentoAjax(this IWebDriver driver)
        {
            var wait = new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(240));
            wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
            waitLoadPage(null);
        }

        public static void IrParaTopoDaPagina(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)BrowserFactory.Driver).ExecuteScript("window.scrollTo(0, 0)");
        }

        public static void ClicarPorJavascript(this IWebDriver driver, IWebElement elemento)
        {
            IJavaScriptExecutor exec = (IJavaScriptExecutor)BrowserFactory.Driver;
            exec.ExecuteScript("arguments[0].Clicar();", elemento);
        }

        public static void SelecionarComboManualmente(this IWebElement elemento, string opcao)
        {
            opcao = PrepararStringParaSelecaoComboManual(opcao);

            elemento.Clicar();
            elemento.Clicar(500);
            BrowserFactory.Driver.pressKey("Home");

            string opcaoSelecionada;
            do
            {
                string ultimaOpcao = elemento.Text;
                BrowserFactory.Driver.pressKey("Down");
                opcaoSelecionada = elemento.Text;
                //Assert necessário pois o sistema pode ter algum problema ao selecionar a opção manualmente e entrar em loop infinito
                //Na primeira vez que o sistema identifica que o texto da opção selecionada anteriormente é o mesmo do atual, ele avança mais uma opção.
                //Caso esta opção continue com o mesmo texto, é porquê é a última opção do combo.
                //GAMBIARRA DAS GRANDES!!!!!!!!
                //Estes IF's são necessários pois existem casos com 4 modelos com o mesmo nome cadastrados.
                if (ultimaOpcao.Equals(opcaoSelecionada))
                {
                    BrowserFactory.Driver.pressKey("Down");
                    opcaoSelecionada = elemento.Text;
                    if (ultimaOpcao.Equals(opcaoSelecionada))
                    {
                        BrowserFactory.Driver.pressKey("Down");
                        opcaoSelecionada = elemento.Text;
                    }
                    if (ultimaOpcao.Equals(opcaoSelecionada))
                    {
                        BrowserFactory.Driver.pressKey("Down");
                        opcaoSelecionada = elemento.Text;
                    }
                }
                Assert.AreNotEqual(ultimaOpcao, opcaoSelecionada);
            } while (!opcaoSelecionada.Equals(opcao));
        }

        public static By ConvertToBy(this IWebElement element)
        {
            if (element == null) throw new NullReferenceException();

            var attributes =
                ((IJavaScriptExecutor)BrowserFactory.Driver).ExecuteScript(
                    "var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;",
                    element) as Dictionary<string, object>;
            if (attributes == null) throw new NullReferenceException();

            var selector = "//" + element.TagName;
            selector = attributes.Aggregate(selector, (current, attribute) =>
                 current + "[@" + attribute.Key + "='" + attribute.Value + "']");

            return By.XPath(selector);
        }

        //MÉTODOS PRIVADOS
        private static void interagirElemento(IWebElement element)
        {
            pageFullLoaded = null;
            waitLoadPage(pageFullLoaded);
            try
            {
                highlightElement(element);
            }
            catch (StaleElementReferenceException)
            {
                BrowserFactory.Driver.Esperar(3000);
                highlightElement(element);
            }
        }

        private static void highlightElement(IWebElement element)
        {
            IJavaScriptExecutor jsElement = (IJavaScriptExecutor)BrowserFactory.Driver;
            string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: red"";";
            jsElement.ExecuteScript(highlightJavascript, new object[] { element });
        }

        private static void waitLoadPage(IWebElement pageReady)
        {
            if (pageReady != null)
                BrowserFactory.Wait.Until(ExpectedConditions.StalenessOf(pageReady));

            var waitForDocumentReady = new WebDriverWait(BrowserFactory.Driver, new TimeSpan(0, 0, 40));
            waitForDocumentReady.Until((wdriver) => (BrowserFactory.Driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete"));

            pageReady = BrowserFactory.Driver.FindElement(By.TagName("html"));

        }

        private static void waitLoadPageCotacao(IWebElement pageReady)
        {
            if (pageReady != null)
                BrowserFactory.Wait.Until(ExpectedConditions.StalenessOf(pageReady));

            var waitForDocumentReady = new WebDriverWait(BrowserFactory.Driver, new TimeSpan(0, 0, 240));
            waitForDocumentReady.Until((wdriver) => (BrowserFactory.Driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete"));

            pageReady = BrowserFactory.Driver.FindElement(By.TagName("html"));

        }


        private static string PrepararStringParaSelecaoComboManual(string texto)
        {
            //Este método remove espaços duplicados e espaços no fim do nome da string.
            //Na seleção manual, este tratamento é necessário.

            //Removendo espaços duplicados
            texto = texto.Replace("  ", " ");

            //Verificando se o último caracter é um espaço em branco. Caso seja, o remove.            
            string ultimaLetra = texto.Substring(texto.Length - 1);
            while (ultimaLetra.Equals(" "))
            {
                texto = texto.Remove(texto.Length - 1);
                ultimaLetra = texto.Substring(texto.Length - 1);
            }

            return texto;
        }
        public static void verifyErroMessageEcommerce(this IWebDriver driver)
        {
            waitForPageToBeFullyLoaded(60);

            bool mensagemErroExibida = BrowserFactory.Driver.VerificarElementoExibido(By.XPath("//span[@class='toast-message'][contains(text(), 'Desculpe, estamos com problemas.')]"));
            Assert.IsFalse(mensagemErroExibida, "Mensagem de Erro Exibida");
        }

        //PRIVATE METHODS
        private static void waitForPageToBeFullyLoaded(int timeSpan)
        {
            BrowserFactory.Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("/html/body/app-root/div[1]/div[@class='loader']")));
            BrowserFactory.Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//p[@class='loading-message']")));

            new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(timeSpan)).Until(
                d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

    }
}
