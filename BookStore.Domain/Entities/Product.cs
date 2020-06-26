using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Product
    {
        //[HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage ="Price is bigger than zero")]
        public decimal Price { get; set; }

        public List<Category> Categories { get; set; }
        //public Category Category { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Product()
        {
            Categories = new List<Category>();
        }
    }
}
