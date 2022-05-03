using FrizzApp.Data.Entities;
using System.Collections.Generic;


namespace FrizzApp.Data.Interfaces
{
    public interface IProductStatusRepository
    {
        void Create(ProductStatus entity);
        List<ProductStatus> GetAll();
        string Delete(ProductStatusEnum id);
    }
}
