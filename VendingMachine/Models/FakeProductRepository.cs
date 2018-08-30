using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    public class FakeProductRepository : IProductRepository {

        public IQueryable<Product> Products => new List<Product> {
            new Product { Name = "Coca-Cola", Price = 54, ImageFilePath = "/lib/images/cocacola.jpg" },
            new Product { Name = "Fanta", Price = 63, ImageFilePath = "/lib/images/fanta.jpg" },
            new Product { Name = "Sprite", Price = 72, ImageFilePath = "/lib/images/sprite.jpg" },
            new Product { Name = "Pepsi", Price = 47, ImageFilePath = "/lib/images/pepsi.jpg" }
        }.AsQueryable();

        public void Decrease(int productId) {
            throw new NotImplementedException();
        }

        public Product DeleteProduct(int productId) {
            throw new NotImplementedException();
        }

        public void Increase(int productId) {
            throw new NotImplementedException();
        }

        public void SaveProduct(Product product) {
            throw new NotImplementedException();
        }

        public void SetQuantity(int productId, int quantity) {
            throw new NotImplementedException();
        }
    }
}
