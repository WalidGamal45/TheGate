﻿using Application.Interfaces;
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
                var code = new Random().Next(100000, 999999).ToString();
                var user = new User()
                {
                    UserName = userDto.UserName,
                    PassWord = userDto.PassWord,
                     Email = userDto.Email,
                    PhoneNumber = userDto.PhoneNumber,
                    VerificationCode = code,
                    IsConfirmed = false
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

        public void Edit(User user, int id)
        {
            var olduser = GetById(id);
            if (olduser != null)
            {
                olduser.UserName = user.UserName;
                olduser.PassWord = user.PassWord;
            }
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
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
            
            return _context.Users.ToList();
        }

        public IEnumerable<User> GetConfirmedUsers()
        {

            return _context.Users.Where(x => x.IsConfirmed).ToList();
        }



        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
