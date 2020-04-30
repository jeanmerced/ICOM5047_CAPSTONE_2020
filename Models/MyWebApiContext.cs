using Microsoft.EntityFrameworkCore;

namespace ICTS_API.Models
{
    public class MyWebApiContext : DbContext
    {
        public MyWebApiContext(DbContextOptions<MyWebApiContext> options)
            : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LocationHistory> LocationHistories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Site> Sites { get; set; }
    }
}
