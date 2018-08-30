using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.Models {

    public interface IProductRepository {

        IQueryable<Product> Products { get; }

        void Decrease(int productId);
        void Increase(int productId);
        void SetQuantity(int productId, int quantity);
        void SaveProduct(Product product);
        Product DeleteProduct(int productId);
    }
}
