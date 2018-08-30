using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.Models.ViewModels {

    public class AdminViewModel {
        public IQueryable<Product> Products { get; set; }
        public IQueryable<Coin> Coins { get; set; }
    }
}
