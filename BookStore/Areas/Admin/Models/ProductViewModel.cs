using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}