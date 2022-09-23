using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //Injeção de dependendia.
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.findAll(); // Acessando o model e pegando os dados da lista, para depois encaminhar para a View();
            return View(list); // passando a lista para a view
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost] // Indicando que esse método é um post
        [ValidateAntiForgeryToken] // Protegendo o post 
        public IActionResult Create(Seller seller)
        {   // Efetivando a inserção no banco de dados da criação dos vendedores
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // Após criar, vai voltar para a tela index do Sellers
        }

        public IActionResult Delete(int? id)
        {   // Verificando se o id é nulo
            if(id == null)
            {
                return NotFound();
            }
            // Buscando o objeto pelo id.
            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return NotFound();
            }
            // Retornando para a View a busca.
            return View(obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            /* Caso o botão de excluir seja clicado, será excluido e redirecionado para o Index */
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            // Verificando se o id é nulo
            if (id == null)
            {
                return NotFound();
            }
            // Buscando o objeto pelo id.
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            // Retornando para a View a busca.
            return View(obj);

        }
    }
}
