using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data
{
    public class Customer:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        ICollection<ShoppingCandy> ShoppingCandies { get; set;}
        ICollection<ShoppingCake> ShoppingCakes { get; set; }
    }
}
