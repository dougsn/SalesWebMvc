using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;

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

        public List<Department> FindAll()
        {   // Pegando a lista de departamento ordenada por nome.
            return _context.Department.OrderBy(x => x.Name).ToList();
        }


    }
}
