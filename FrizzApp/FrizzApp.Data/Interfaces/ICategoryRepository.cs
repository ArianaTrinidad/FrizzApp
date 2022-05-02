using FrizzApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrizzApp.Data.Interfaces
{
    public interface ICategoryRepository
    {
        void CreateCategory(Category entity);
        List<Category> GetAll();

        string DeleteCategory(int Id);
    }
}