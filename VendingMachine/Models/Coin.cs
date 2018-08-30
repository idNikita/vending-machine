using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VendingMachine.Models {

    public enum CoinName {
        One,
        Two,
        Five,
        Ten
    }

    public class Coin {
        public int CoinID { get; set; }
        public CoinName Name { get; set; }
        public int Nominal { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Количество должно быть больше 0")]
        [Display(Name = "Остаток")]
        public int Count { get; set; }

        [Required]        
        [Display(Name = "Принимать")]
        public bool IsAvailable { get; set; }
    }
}
