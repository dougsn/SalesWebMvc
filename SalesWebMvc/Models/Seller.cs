using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller /*Exemplos para validar os atributos.*/
    {
        // O {0} se refere ao nome (Primeiro atributo), o {2} se refere ao tamanho minimo que é 3, e o {1} se refere ao tamanho máximo que é o 60.

        public int Id { get; set; }
        [Required(ErrorMessage = "{0} required")] // Indicando que esse atributo é obrigatório
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}" )]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage ="Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string  Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")] // Configurando a forma que o atributo aparecerá em nossa View()
        [DataType(DataType.Date)] // Configurando o formata para preencher a data no formulário
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate{ get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")] // O salário tem que ser no minimo 1100 e max 50000
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")] // Indicando que esse atributo terá 2 casas decimais
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime inital, DateTime final)
            // Calculando o total de vendas de um vendedor, e um determinado intervalo de data.
        {   // Filtrando a lista de SalesRecord pelo intervalo das datas e somando os valores de cada um
            return Sales.Where(sr => sr.Date >= inital && sr.Date <= final).Sum(sr => sr.Amount);

        }


    }
}
