using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //Injeção de dependencia.
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.findAlAsync(); // Acessando o model e pegando os dados da lista, para depois encaminhar para a View();
            return View(list); // passando a lista para a view
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost] // Indicando que esse método é um post
        [ValidateAntiForgeryToken] // Protegendo o post 
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid) { // Se o Seller não for válido, ele volta para a tela inicial
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };
                return View(viewModel);
            }

            // Efetivando a inserção no banco de dados da criação dos vendedores
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index)); // Após criar, vai voltar para a tela index do Sellers
        }

        public async Task<IActionResult> Delete(int? id)
        {   // Verificando se o id é nulo
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }
            // Buscando o objeto pelo id.
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); 
            }
            // Retornando para a View a busca.
            return View(obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            /* Caso o botão de excluir seja clicado, será excluido e redirecionado para o Index */
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            // Verificando se o id é nulo
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not providad" }); 
            }
            // Buscando o objeto pelo id.
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            // Retornando para a View a busca.
            return View(obj);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not providad" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid) { // Se o Seller não for válido, ele volta para a tela inicial
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller};
                return View(viewModel);
            }

            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not mismatch" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
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

        public async Task<IActionResult> Error(string message)
        {                                                           // Pegando o ID interno da requisição
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier};
            return View(viewModel);
        }

    }
}
