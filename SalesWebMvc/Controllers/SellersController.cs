using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //Injeção de dependendia.
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.findAll(); // Acessando o model e pegando os dados da lista, para depois encaminhar para a View();
            return View(list); // passando a lista para a view
        }
    }
}
