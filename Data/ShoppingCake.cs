namespace WebApplication1.Data
{
    public class ShoppingCake
    {
        public int Id { get; set; } 

        public int CakeId { get; set; }
        public Cake Cakes { get; set; }

        public string CustomerId { get; set; }
        public Customer Customers { get; set; }

        public string Quality { get; set; }
        public int Size { get; set; }
        public string Requirement { get; set; }
        public DateTime RegisterOn { get; set; }
    }
}
