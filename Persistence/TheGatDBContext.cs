
using Domain.Domains;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class TheGatDBContext : DbContext
    {
        
        public TheGatDBContext(DbContextOptions<TheGatDBContext> options) : base(options) { }

        public DbSet<Admin> admins {  get; set; }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<SubCategory> SubCategories {  get; set; }
        public DbSet<Product> Products {  get; set; }
        public DbSet<User> Users {  get; set; }
        public DbSet<CartItem> CartItems {  get; set; }

    }
}
