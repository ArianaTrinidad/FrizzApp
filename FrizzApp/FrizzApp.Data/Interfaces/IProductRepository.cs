using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FrizzApp.Data.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll(int pageNumber, int pageSize, string palabraClave, decimal preciomin, decimal preciomax, int categoria);
        void Create(Product entity);
        void Update(Product entity);
        string Delete(int id);
        bool ChangeStatus(int dto);
    }
}