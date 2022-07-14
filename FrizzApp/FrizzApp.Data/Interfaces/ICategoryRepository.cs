using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FrizzApp.Data.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        void CreateCategory(Category entity);

        bool DeleteCategory(int Id);
    }
}