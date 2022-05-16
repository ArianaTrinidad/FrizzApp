using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FrizzApp.Data.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll(string search, int pageNumber, int pageSize,
            string palabraClave = default, decimal preciomin = default, decimal preciomax = default, string categoria = default);
        void Create(Product entity);
        string Delete(int id);
    }
}