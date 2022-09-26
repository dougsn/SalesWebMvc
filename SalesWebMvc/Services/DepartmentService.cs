using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        // Injeção de dependencia
        private SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync() // O Task é o prefixo para criarmos ua função assincrona.
        {   // Pegando a lista de departamento ordenada por nome.
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }


    }
}
