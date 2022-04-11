using SeleniumExtras.PageObjects;
using Webmotors.WrapperFactory;



//Controla todas as paginas do projeto
namespace Webmotors.PageObjects
{
    public class Page
    {
        
        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(BrowserFactory.Driver, page);
            return page;
        }
        public static ComprarVeiculo ComprarVeiculo
        {
            get
            { return GetPage<ComprarVeiculo>(); }//Pagina Comprar veiculo Webmotors

        }
    }
    
}
