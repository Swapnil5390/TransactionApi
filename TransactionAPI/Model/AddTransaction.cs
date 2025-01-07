namespace TransactionAPI.Model
{
    public class AddTransaction
    {
            
        public int Price { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public int IsActive { get; set; }
    }
}

