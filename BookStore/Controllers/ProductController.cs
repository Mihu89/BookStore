using BookStore.Domain.Entities;
using BookStore.Domain.Implementation;
using BookStore.Domain.Implementation.Repo;
using BookStore.Domain.Interfaces;
using BookStore.Models;
using Microsoft.Ajax.Utilities;
using System;
using BookStore.Domain.Implementation.Repo;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class ProductController : Controller
    {
        // private IProductRepository productRepository;
        private UnitOfWork<Product> unitOfWork;
        private UnitOfWork<Category> unitOfCategory;
        public int PageSize = 5;
        public Category CurrentCategory { get; set; }

        public ProductController() //IProductRepository repo)
        {
            //this.productRepository = repo;
            unitOfWork = new UnitOfWork<Product>(new GenericRepository<Product>(new ApplicationDbContext()));
            unitOfCategory = new UnitOfWork<Category>(new GenericRepository<Category>(new ApplicationDbContext()));
            CurrentCategory = unitOfCategory.GenericRepository.FirstOrDefault();
        }

        // GET: Product
        public ViewResult List(int categoryId = 1, int page = 1)
        {
            var source = unitOfWork.GenericRepository.Get();

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
                    TotalItems = unitOfWork.GenericRepository.Get((x) => x.Categories.Any(y => y.Id == categoryId)).Count()
                    // productRepository.Products
                    //            .Where(p => p.Categories.Any(x => x.Id == categoryId)).Count()
                },
                Category = CurrentCategory
            };
            return View(model);
        }
    }
}