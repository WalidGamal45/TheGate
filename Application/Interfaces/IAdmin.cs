using Domain.Domains;
using Domain.DTOs;

namespace Application.Interfaces
{
    public interface IAdmin
    {
        IEnumerable<Admin> GetAdmins();
        Admin GetById(int id);
        void Add(AddAdminDto admin);
        void Update(UpdateAdminDto admin);
        void Delete(int id);



    }
}
