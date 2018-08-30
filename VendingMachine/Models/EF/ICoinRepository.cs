using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.Models {

    public interface ICoinRepository {

        IQueryable<Coin> Coins { get; }

        void SaveCoin(Coin coin);
        void AddCoin(CoinName name);
        void DeleteCoin(CoinName name);
        void SetQuantity(CoinName name, int count);
        decimal ComputeTotal();
        Dictionary<string, int> ComputeChange(decimal value);
    }
}
