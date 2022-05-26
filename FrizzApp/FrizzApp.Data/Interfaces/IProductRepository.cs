using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FrizzApp.Data.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll(string search,
                             int? category,
                             decimal? priceMin,
                             decimal? priceMax,
                             int pageNumber,
                             int pageSize);
        void Create(Product entity);
        string Delete(int id);
    }
}