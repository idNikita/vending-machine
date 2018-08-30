using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.Models {

    public class VendingMachineViewModel {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Coin> Coins { get; set; }
        public Balance Balance { get; set; }
    }
}
