using System.Linq;

namespace VendingMachine.Models {

    public class EFProductRepository : IProductRepository {

        private ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext cxt) => context = cxt;

        public IQueryable<Product> Products => context.Products;

        public void Decrease(int productId) {
            Product dbEntry = context.Products.FirstOrDefault(p => p.ProductID == productId);
            if (dbEntry != null) {
                dbEntry.Count -= 1;
                context.SaveChanges();
            }                        
        }

        public void Increase(int productId) {
            Product dbEntry = context.Products.FirstOrDefault(p => p.ProductID == productId);
            if(dbEntry != null) {
                dbEntry.Count += 1;
                context.SaveChanges();
            }            
        }

        public Product DeleteProduct(int productId) {
            Product dbEntry = context.Products.FirstOrDefault(p => p.ProductID == productId);
            if (dbEntry != null) {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }        

        public void SaveProduct(Product product) {
            if (product.ProductID == 0) {
                context.Products.Add(product);
            }
            else {
                Product dbEntry = context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null) {
                    dbEntry.Name = product.Name;
                    dbEntry.Price = product.Price;
                    dbEntry.ImageFilePath = product.ImageFilePath;
                    dbEntry.Count = product.Count;
                }
            }
            context.SaveChanges();
        }

        public void SetQuantity(int productId, int quantity) {
            Product dbEntry = context.Products.FirstOrDefault(p => p.ProductID == productId);
            if (dbEntry != null) {
                dbEntry.Count = quantity;
                context.SaveChanges();
            }            
        }
    }
}
