using Application.Interfaces;
using Domain.Domains;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
{
    public class ProductServices : IProduct
    {
        private readonly AdminDBContext _dbContext;
        public ProductServices(AdminDBContext context)
        {
            _dbContext = context;
        }
        public void Add(Product admin)
        {
           _dbContext.Products.Add(admin);
        }

        public void Delete(int id)
        {
            var pro = GetById(id);
            _dbContext.Remove(pro);
        }

        public IEnumerable<Product> GetAll()
        {
           var pro = _dbContext.Products.ToList();
            return pro;
        }

        public Product GetById(int id)
        {
           var pro = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            return pro;
        }

        public void Update(Product admin)
        {
            _dbContext.Update(admin);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
