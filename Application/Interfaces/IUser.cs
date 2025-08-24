using Domain.Domains;
using Domain.DTOs.Users;

namespace Application.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> GetUsers();
        User GetById(int id);
        void Add(UserDto user);
        void Edit(User user,int id);
        void Delete(int id);
        void Save();
        User GetByEmail(string email);

    }
}
