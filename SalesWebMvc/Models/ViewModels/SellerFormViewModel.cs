using System.Collections.Generic;

namespace SalesWebMvc.Models.ViewModels
{
    // Classe que servirá para criarmos o formulários de cadastro de vendedor
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
