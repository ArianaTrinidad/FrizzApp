using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FrizzApp.Data.Interfaces
{
    public interface ICategoryRepository
    {
        void CreateCategory(Category entity);
        List<Category> GetAll();

        string DeleteCategory(int Id);
    }
}