
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using System.Linq;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            // Criando um objeto IQuerable para realizar a filtragem

            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue) // Se foi informado uma data minima, pega os obj que sejam menor ou igual a esta data
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue) // Se  foi informado uma data máxima, pega os obj que sejam maior ou igual a esta data
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            // Realizando o join entre a tabela de vendedor com o departamento, ordenando por data.
            return await result.Include(x => x.Seller)
                        .Include(x => x.Seller.Department)
                        .OrderByDescending(x => x.Date)
                        .ToListAsync();

        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
