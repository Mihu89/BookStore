using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Implementation.Repo
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public IEnumerable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }

        public void DeleteProduct(Product product)
        {
            var productToDelete = context.Products.Find(product.Id);
            if (productToDelete != null)
            {
                context.Products.Remove(productToDelete);
                context.SaveChanges();
            }
        }

        public Category GetPrincipalCategory(Product product)
        {
            if (product != null)
            {
                return context.Categories
                 .FirstOrDefault(x => x.Id == product.Categories[0].Id);
            }
            return context.Categories
                .FirstOrDefault();
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == Guid.Empty)
            {
                context.Products.Add(product);
            }
            else
            {
                var existingProduct = context.Products.Find(product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Categories = product.Categories;
                    existingProduct.Price = product.Price;
                    existingProduct.CreatedDate = product.CreatedDate;
                    existingProduct.ExpirationDate = product.ExpirationDate;
                }
            }
            context.SaveChanges();
        }
    }
}
