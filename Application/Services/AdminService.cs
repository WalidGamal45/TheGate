using Application.Interfaces;
using Domain.Domains;
using Domain.DTOs;
using Persistence;

namespace Application.Services
{
    public class AdminService :IAdmin
    {
        private readonly AdminDBContext dBContext;

        public AdminService(AdminDBContext adminDB)
        {
            dBContext = adminDB;
        }

        public void Add(AddAdminDto adminDto)
        {
            var admin = new Admin
            {
                Username = adminDto.Username,
                Password = adminDto.Password,
            };

            dBContext.admins.Add(admin);
            dBContext.SaveChanges(); 

            
        }

        public void Delete(int id)
        {
            var admin = dBContext.admins.FirstOrDefault(x => x.Id == id);
            if (admin != null)
            {
                dBContext.admins.Remove(admin);
                dBContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Admin not found.");
            }
        }

        public IEnumerable<Admin> GetAdmins()
        {
            return dBContext.admins.ToList();
        }

        public Admin GetById(int id)
        {
            return dBContext.admins.FirstOrDefault(a => a.Id == id);
        }

        public void Update(UpdateAdminDto dto)
        {
            var admin = dBContext.admins.FirstOrDefault(x => x.Id == dto.Id);
            if (admin != null)
            {
                admin.Username = dto.Username;
                admin.Password = dto.Password;

                dBContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Admin not found.");
            }
        }
    }
}
