using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] // Indicando que esse método é um post
        [ValidateAntiForgeryToken] // Protegendo o post 
        public IActionResult Create(Seller seller)
        {   // Efetivando a inserção no banco de dados da criação dos vendedores
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // Após criar, vai voltar para a tela index do Sellers
        }
    }
}
