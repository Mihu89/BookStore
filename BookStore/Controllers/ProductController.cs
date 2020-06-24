using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Models;
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
        public ViewResult List(int categoryId, int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = productRepository.Products
                .Where(p => p.Categories.Any(x => x.Id == categoryId))
                .OrderBy(p => p.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = productRepository.Products
                    .Where(x => x.Categories
                        .All(c => c.Id == categoryId)).Count()
                },
                Category = CurrentCategory
            };
            return View(model);
        }
    }
}