using Domain.Domains;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProduct
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product admin);
        void Update(Product admin);
        void Delete(int id);

    }
}
