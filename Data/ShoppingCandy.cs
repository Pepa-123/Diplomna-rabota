namespace WebApplication1.Data
{
    public class ShoppingCandy
    {
        public int Id { get; set; }

        public int CandiesId { get; set; }
        public Candy Candies { get; set; }

        public string CustomersId { get;set; }
        public Customer Customers { get; set; }

        public int Size { get; set; }   
        public DateTime RegisterOn { get; set; }
    }
}
