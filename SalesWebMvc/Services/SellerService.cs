using Microsoft.EntityFrameworkCore;
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

        // Classe responsavel por criar/inserir os objetos no banco de dados.
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        { // Pegando a lista de vendedores, filtrando pelo Id deles com o que foi passado por parâmetro, para retornar.
            
            return _context.Sellers.Include(s => s.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            // Pegando o vendedor pelo ID
            var obj = _context.Sellers.Find(id);
            // para dps remover o vendedor selecionado pelo id
            _context.Sellers.Remove(obj);
            // Salvando a alteração no banco de dados.
            _context.SaveChanges();
        }
    }
}
