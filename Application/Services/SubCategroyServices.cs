using Application.Interfaces;
using Domain.Domains;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
{
    public class SubCategroyServices:ISubCategory
    {
        private readonly TheGatDBContext _context;
        public SubCategroyServices(TheGatDBContext category)
        {
            _context = category;
        }

        public void Add(SubCategory admin)
        {
            _context.Add(admin);
        }

        public void Delete(int id)
        {
           var sub = GetById(id);
            _context.Remove(sub);
        }

        public IEnumerable<SubCategory> GetAll()
        {
            var sub = _context.SubCategories.Include(x=>x.category).ToList();
            return sub;
        }

        public SubCategory GetById(int id)
        {
           var pro= _context.SubCategories.FirstOrDefault(p => p.Id == id);
            return pro;

        }

        public void Update(SubCategory admin)
        {
           _context.Update(admin);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
