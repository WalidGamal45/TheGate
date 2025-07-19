using Domain.Domains;
using Domain.DTOs;

namespace Application.Interfaces
{
    public interface ICategory
    {
        IEnumerable<Category> GetAdmins();
        Category GetById(int id);
        void Add(Category admin);
        void Update(Category admin);
        void Delete(int id);
        void Save();

    }
}
