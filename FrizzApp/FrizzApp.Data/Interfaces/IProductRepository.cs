using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FrizzApp.Data.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        void Create(Product entity);
        string Delete(int id);
    }
}