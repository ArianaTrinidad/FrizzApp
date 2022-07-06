using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FrizzApp.Data.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        void Create(Order entity);
    }
}
