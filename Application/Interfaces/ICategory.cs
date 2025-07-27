﻿using Domain.Domains;
using Domain.DTOs;
using Domain.DTOs.Category;

namespace Application.Interfaces
{
    public interface ICategory
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Add(Category admin);
        void Update(Category admin);
        void Delete(int id);
        void Save();

    }
}
