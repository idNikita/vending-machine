using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace VendingMachine.Models {

    public static class SeedData {

        public static void EnsurePopulated(IApplicationBuilder app) {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            if (!context.Coins.Any()) {
                context.Coins.AddRange(
                    new Coin { Name = CoinName.One, Nominal = 1, Count = 0 },
                    new Coin { Name = CoinName.Two, Nominal = 2, Count = 0 },
                    new Coin { Name = CoinName.Five, Nominal = 5, Count = 0 },
                    new Coin { Name = CoinName.Ten, Nominal = 10, Count = 0 }
                );
            }

            if (!context.Products.Any()) {
                context.Products.AddRange(
                    new Product { Name = "Coca-Cola", Price = 54, ImageFilePath = "/lib/images/cocacola.jpg" },
                    new Product { Name = "Fanta", Price = 63, ImageFilePath = "/lib/images/fanta.jpg" },
                    new Product { Name = "Sprite", Price = 72, ImageFilePath = "/lib/images/sprite.jpg" },
                    new Product { Name = "Pepsi", Price = 47, ImageFilePath = "/lib/images/pepsi.jpg" }
                );                
            }

            context.SaveChanges();
        }
    }
}
