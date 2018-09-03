using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using VendingMachine.Models;
using System.Linq;
using System;

namespace VendingMachine.Controllers {

    public class HomeController : Controller {        
        private IProductRepository productRepository;
        private ICoinRepository coinRepository;
        private Balance userBalance;

        public HomeController(IProductRepository pRepo, ICoinRepository cRepo, Balance balanceService) {
            productRepository = pRepo;
            coinRepository = cRepo;
            userBalance = balanceService;
        }

        public ViewResult Index() => View(new VendingMachineViewModel { Balance = userBalance, Products = productRepository.Products, Coins = coinRepository.Coins });

        [HttpPost]
        public RedirectToActionResult AddCoin(CoinName name) {
            coinRepository.AddCoin(name);
            userBalance.Add(coinRepository.Coins.Where(c => c.Name == name).FirstOrDefault().Nominal);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult Clear() {
            userBalance.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult BuyProduct(int id) {
            if (userBalance.Count > 0) {
                if (productRepository.Products.Where(p => p.ProductID == id).FirstOrDefault().Count > 0) {
                    userBalance.Remove(productRepository.Products.Where(p => p.ProductID == id).FirstOrDefault().Price);
                    productRepository.Decrease(id);
                }
            }            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GiveChange() => Json(new { coins = coinRepository.ComputeChange(userBalance.Count), balance = userBalance.Count.ToString("c") });
    }
}