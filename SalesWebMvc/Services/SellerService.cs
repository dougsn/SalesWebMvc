using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> findAll()
        {
            return _context.Sellers.ToList(); // Acessando a fonte de dados dos vendedores e convertendo para lista.
        }
    }
}
