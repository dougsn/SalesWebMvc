using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Collections.Generic;
using System.Diagnostics;

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
        {
            if (!ModelState.IsValid) { // Se o Seller não for válido, ele volta para a tela inicial
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };
                return View(viewModel);
            }

            // Efetivando a inserção no banco de dados da criação dos vendedores
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // Após criar, vai voltar para a tela index do Sellers
        }

        public IActionResult Delete(int? id)
        {   // Verificando se o id é nulo
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }
            // Buscando o objeto pelo id.
            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); 
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
                return RedirectToAction(nameof(Error), new { message = "Id not providad" }); 
            }
            // Buscando o objeto pelo id.
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            // Retornando para a View a busca.
            return View(obj);

        }
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not providad" });
            }

            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid) { // Se o Seller não for válido, ele volta para a tela inicial
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller};
                return View(viewModel);
            }

            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not mismatch" });
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {

                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public IActionResult Error(string message)
        {                                                           // Pegando o ID interno da requisição
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier};
            return View(viewModel);
        }

    }
}
