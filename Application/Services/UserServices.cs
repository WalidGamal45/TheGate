using Application.Interfaces;
using Domain.Domains;
using Domain.DTOs.Users;
using Persistence;

namespace Application.Services
{
    public class UserServices : IUser
    {
        private readonly TheGatDBContext _context;

        public UserServices(TheGatDBContext context)
        {
            _context = context;
        }

        public void Add(UserDto userDto)
        {
            if (userDto != null)
            {
                var user = new User()
                {
                    UserName = userDto.UserName,
                    PassWord = userDto.PassWord
                };
                _context.Users.Add(user);
            }
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public void Edit(UserDto user, int id)
        {
            var olduser = GetById(id);
            if (olduser != null)
            {
                olduser.UserName = user.UserName;
                olduser.PassWord = user.PassWord;
            }
        }

        public User GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} was not found.");
            }
            return user;
        }


        public IEnumerable<User> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
