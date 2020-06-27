using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepository;
        public int PageSize = 5;
        public Category CurrentCategory { get; set; }

        public ProductController(IProductRepository repo)
        {
            this.productRepository = repo;
            CurrentCategory = repo.GetPrincipalCategory(null);
        }

        // GET: Product
        public ViewResult List(int categoryId = 1, int page = 1)
        {
            var source = productRepository.Products;
               // .Where(p => p.Categories.Contains(CurrentCategory));

            ProductListViewModel model = new ProductListViewModel
            {
                Products = source
                .OrderBy(p => p.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = productRepository.Products
                                .Where(p => p.Categories.Any(x => x.Id == categoryId)).Count()
                },
                Category = CurrentCategory
            };
            return View(model);
        }
    }
}