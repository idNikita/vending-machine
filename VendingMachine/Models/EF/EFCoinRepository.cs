using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.Models {

    public class EFCoinRepository : ICoinRepository {

        private ApplicationDbContext context;

        public EFCoinRepository(ApplicationDbContext cxt) => context = cxt;

        public IQueryable<Coin> Coins => context.Coins;

        public void AddCoin(CoinName name) {
            context.Coins.Where(c => c.Name == name).FirstOrDefault().Count += 1;
            context.SaveChanges();
        }

        public void DeleteCoin(CoinName name) {
            context.Coins.Where(c => c.Name == name).FirstOrDefault().Count += 1;
            context.SaveChanges();
        }

        public void SetQuantity(CoinName name, int count) {
            context.Coins.Where(c => c.Name == name).FirstOrDefault().Count = count;
            context.SaveChanges();
        }

        public void SaveCoin(Coin coin) {
            if (coin.CoinID == 0) {
                context.Coins.Add(coin);
            }
            else {
                Coin dbEntry = context.Coins.FirstOrDefault(c => c.CoinID == coin.CoinID);
                if (dbEntry != null) {
                    dbEntry.IsAvailable = coin.IsAvailable;
                    dbEntry.Count = coin.Count;
                }
            }
            context.SaveChanges();
        }

        public decimal ComputeTotal() => context.Coins.Sum(c => c.Count * c.Nominal);

        public Dictionary<string, int> ComputeChange(decimal value) {
            Dictionary<string, int> requiredCoins = new Dictionary<string, int> {
                ["Ten"] = 0,
                ["Five"] = 0,
                ["Two"] = 0,
                ["One"] = 0
            };

            Coin ten = context.Coins.Where(c => c.Name == CoinName.Ten).FirstOrDefault();
            Coin five = context.Coins.Where(c => c.Name == CoinName.Five).FirstOrDefault();
            Coin two = context.Coins.Where(c => c.Name == CoinName.Two).FirstOrDefault();
            Coin one = context.Coins.Where(c => c.Name == CoinName.One).FirstOrDefault();            

            if (value < ComputeTotal()) {
                decimal sum = value;

                while (sum > 0) {
                    while (sum >= ten.Nominal && ten.Count > 0) {
                        sum -= ten.Nominal;
                        ten.Count -= 1;
                        requiredCoins["Ten"] += 1;
                    }
                    while (sum >= five.Nominal && five.Count > 0) {
                        sum -= five.Nominal;
                        five.Count -= 1;
                        requiredCoins["Five"] += 1;
                    }
                    while (sum >= two.Nominal && two.Count > 0) {
                        sum -= two.Nominal;
                        two.Count -= 1;
                        requiredCoins["Two"] += 1;
                    }
                    while (sum >= one.Nominal && one.Count > 0) {
                        sum -= one.Nominal;
                        one.Count -= 1;
                        requiredCoins["One"] += 1;                        
                    }
                }
                context.SaveChanges();
            }

            return requiredCoins;
        }
    }
}