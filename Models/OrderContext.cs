using Microsoft.EntityFrameworkCore;

namespace Order_Details.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; } = null!;
    }
}
