using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FrizzApp.Data.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll(int pageNumber, int pageSize, string palabraClave, decimal preciomin, decimal preciomax, int categoria);
        Product GetById(int id);
        void Create(Product entity);
        void ActualizePrice(int Percentage);
        bool Update(Product entity);
        bool ChangeStatus(int dto);
        bool Delete(int id);
    }
}