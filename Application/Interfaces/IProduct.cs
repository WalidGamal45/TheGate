using Domain.Domains;
using Domain.DTOs;
using Domain.DTOs.Product;
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
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        void Save();

    }
}
