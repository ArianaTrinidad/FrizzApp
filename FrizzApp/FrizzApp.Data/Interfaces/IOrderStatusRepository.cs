using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FrizzApp.Data.Interfaces
{
    public interface IOrderStatusRepository
    {
        void CreateOrderStatus(OrderStatus entity);
        List<OrderStatus> GetAll();

        string DeleteOrderStatus(int statusId);
    }
}
