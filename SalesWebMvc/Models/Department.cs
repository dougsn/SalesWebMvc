using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void addSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {// Calculando o total de vendas do departamento, em um determinado intervalo de datas.
            // Pegando cada vendedor na lista, chamando o TotalSales do vendedor naquele periodo de tempo.
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    } 

}
