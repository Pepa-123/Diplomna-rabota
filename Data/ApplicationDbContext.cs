using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cake> Cakes { get; set;}
        public DbSet<Candy> Cadies { get; set; }
        public DbSet<ShoppingCake> ShoppingCakes { get; set; }

        public DbSet<ShoppingCandy> ShoppingCandies { get; set; } 


    }
}