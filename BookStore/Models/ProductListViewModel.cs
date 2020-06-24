using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Category Category { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}