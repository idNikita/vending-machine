namespace VendingMachine.Models {

    public class Balance {

        public decimal Count { get; set; }

        public virtual void Add(decimal value) {
            Count += value;
        }

        public virtual void Remove(decimal value) {
            if (Count > value) {
                Count -= value;
            }
            else {
                Count = 0;
            }
        }

        public virtual void Clear() {
            Count = 0;
        }
    }
}
