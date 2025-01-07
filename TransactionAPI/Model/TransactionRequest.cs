using System.Text.Json.Serialization;

namespace TransactionAPI.Model
{
    public class TransactionRequest
    {    
        public int Id { get; set; }
         public int Price { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        [JsonIgnore]
        public int IsActive { get; set; }

    }
}
