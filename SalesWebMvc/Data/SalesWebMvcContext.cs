using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Models
{
    public class SalesWebMvcContext : DbContext
    {
        public SalesWebMvcContext (DbContextOptions<SalesWebMvcContext> options)
            : base(options)
        {
        }
        // As 3 entidades do C# para fazer relação com as 3 tabelas que serão criadas no Banco de Dados
        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Sellers{ get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
