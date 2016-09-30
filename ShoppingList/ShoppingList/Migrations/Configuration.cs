namespace ShoppingList.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ShoppingList.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.ShoppingListContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ShoppingListContext context)
        {
            context.Products.AddOrUpdate(x => x.Id,
                new Product() { Id = 1, Name = "Choco", Brand = "Nutella", Description = "Gewone choco"},
                new Product() { Id = 2, Name = "Brood", Description = "Wit brood"},
                new Product() { Id = 3, Name = "Wafels", Description = "Wafels met chocolade"}
                );

            context.ShoppingLists.AddOrUpdate(x => x.Id,
                new Models.ShoppingList()
                {
                    Id = 1,
                    Name = "ShoppingList Vets"
                });
        }
    }
}
