using FrizzApp.Data.Entities;
using System.Collections.Generic;


namespace FrizzApp.Data.Interfaces
{
    public interface IProductStatusRepository
    {
        List<ProductStatus> GetAll();
        void Create(ProductStatus entity);
        string Delete(int id);
    }
}
