using Application.Interfaces;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryServices : ICategory
    {
        private readonly AdminDBContext _context;
        public CategoryServices(AdminDBContext category)
        {
            _context = category;
        }
        public void Add(Category admin)
        {
           _context.Add(admin);
        }

        public void Delete(int id)
        {
            var cat = GetById(id);
            _context.Remove(cat);
        }

        public IEnumerable<Category> GetAll()
        {
           var cat = _context.Categories.ToList();
            return cat;
        }

        public Category GetById(int id)
        {
           var cat= _context.Categories.FirstOrDefault(c => c.Id == id);
            return cat;

        }

        public void Update(Category admin)
        {
           _context.Update(admin);
        }
      

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
