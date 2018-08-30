using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Models;
using VendingMachine.Models.ViewModels;

namespace VendingMachine.Controllers
{
    public class AdminController : Controller {

        private IProductRepository productRepository;
        private ICoinRepository coinRepository;

        public AdminController(IProductRepository pRepo, ICoinRepository cRepo) {
            productRepository = pRepo;
            coinRepository = cRepo;
        }

        public IActionResult Index(string key) {
            if (key == "secret") {
                return View(new AdminViewModel { Products = productRepository.Products, Coins = coinRepository.Coins });
            }
            else {
                return RedirectToAction("Index", "Home");                
            }            
        }

        public IActionResult EditCoin(int id) {
            Coin coin = coinRepository.Coins.Where(c => c.CoinID == id).FirstOrDefault();
            if (coin != null) {
                ViewData["Title"] = "Редактирование";
                return PartialView(coin);
            }
            return RedirectToAction(nameof(Index), new { key = "secret" });
        }

        [HttpPost]
        public IActionResult EditCoin(Coin coin) {
            if (ModelState.IsValid) {
                coinRepository.SaveCoin(coin);
                TempData["message"] = $"{coin.Name} сохранен";
                return RedirectToAction(nameof(Index), new { key = "secret" });
            }
            else {
                return View(coin);
            }
        }

        public IActionResult EditProduct(int id) {
            Product product = productRepository.Products.Where(p => p.ProductID == id).FirstOrDefault();
            if (product != null) {
                ViewData["Title"] = "Редактирование";
                return PartialView(product);
            }
            return RedirectToAction(nameof(Index), new { key = "secret" });
        }

        [HttpPost]
        public IActionResult EditProduct(Product product) {
            if (ModelState.IsValid) {
                productRepository.SaveProduct(product);
                TempData["message"] = $"{product.Name} сохранен";
                return RedirectToAction(nameof(Index), new { key = "secret" });
            }
            else {
                return View(product);
            }
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id) {
            Product deletedProduct = productRepository.DeleteProduct(id);
            if (deletedProduct != null) {
                TempData["message"] = $"{deletedProduct.Name} удален";
            }
            return RedirectToAction(nameof(Index), new { key = "secret" });
        }

        public IActionResult CreateProduct() {
            ViewData["Title"] = "Новый";
            return PartialView("EditProduct", new Product());
        }
    }
}