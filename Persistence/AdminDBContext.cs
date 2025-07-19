
using Domain.Domains;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AdminDBContext : DbContext
    {
        
        public AdminDBContext(DbContextOptions<AdminDBContext> options) : base(options) { }
        public DbSet<Admin> admins {  get; set; }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<SubCategory> SubCategories {  get; set; }
        public DbSet<Product> Products {  get; set; }
        


    }
}
