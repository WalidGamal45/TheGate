﻿using Domain.Domains;
using Domain.DTOs;

namespace Application.Interfaces
{
    public interface ISubCategory
    {
        IEnumerable<SubCategory> GetAll();
        SubCategory GetById(int id);
        void Add(SubCategory admin);
        void Update(SubCategory admin);
        void Delete(int id);
        void Save();

    }
}
