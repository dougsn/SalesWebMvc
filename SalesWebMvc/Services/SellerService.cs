using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> findAlAsync()
        {
            return await _context.Sellers.ToListAsync(); // Acessando a fonte de dados dos vendedores e convertendo para lista.
        }

        // Classe responsavel por criar/inserir os objetos no banco de dados.
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
           await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        { // Pegando a lista de vendedores, filtrando pelo Id deles com o que foi passado por parâmetro, para retornar.
            // O Include realiza o Join entre as tabelas de Seller e Department, para buscar o departamento do vendedor
            return await _context.Sellers.Include(s => s.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)  
        {
            try
            {

            
            // Pegando o vendedor pelo ID
            var obj = _context.Sellers.Find(id);
            // para dps remover o vendedor selecionado pelo id
            _context.Sellers.Remove(obj);
            // Salvando a alteração no banco de dados.
            await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

        public async Task UpdateAsync(Seller obj) // O Any() serve para verificar se existe algum registro no BDD com base no Lambda.
        {   // Se não existir algum vendedor no BDD cujo o ID seja igual ao meu obj
            bool hasAny = await _context.Sellers.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {

                throw new DbConcurrencyException(e.Message);
            }
           

        }
    }
}
    