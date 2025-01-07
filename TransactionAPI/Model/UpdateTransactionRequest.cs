namespace TransactionAPI.Model
{
    public class UpdateTransactionRequest
    {
         
        public int Price { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public int Id { get; set; }
    }
}
