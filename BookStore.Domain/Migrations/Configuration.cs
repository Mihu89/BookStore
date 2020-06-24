namespace BookStore.Domain.Migrations
{
    using BookStore.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookStore.Domain.Implementation.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookStore.Domain.Implementation.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var c1 = new Category
            {
                Name = "Books"
            };
            context.Categories.Add(c1);
            var c2 = new Category
            {
                Name = "Toys"
            };
            context.Categories.Add(c2);

            var c3 = new Category
            {
                Name = "Computers"
            };
            context.Categories.Add(c3);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Voina i Mir",
                Categories = new List<Category> { c1,c2},
                Description = "Bestseller from Lev Tolstoy",
                CreatedDate = new DateTime(1880,10,12),
                ExpirationDate = new DateTime(2020, 10, 12),
                Price = 20
            };
            context.Products.Add(product);

            var product1 = new Product
            {
                //Id = Guid.NewGuid(),
                Name = "Jizni Ilia Ilich",
                Categories = new List<Category> { c1, c2 },
                Description = "Life of Ilia Ilich",
                CreatedDate = new DateTime(1890, 10, 12),
                ExpirationDate = new DateTime(2020, 10, 12),
                Price = 25
            };
            context.Products.Add(product1);

            context.SaveChanges();
        }
    }
}
