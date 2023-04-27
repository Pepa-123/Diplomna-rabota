using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data
{
    public class Cake
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public  string ImageURL { get;set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public DateTime RegisterOn { get; set; }
        ICollection<ShoppingCake> ShoppingCakes { get; set;}
    }
}
