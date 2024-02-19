using Microsoft.EntityFrameworkCore;

namespace pos_point_system.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }
        public virtual DbSet<CouponExchange> CouponExchanges { get; set; }
    }
}
