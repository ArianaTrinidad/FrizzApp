using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FrizzApp.Data.Interfaces
{
    public interface IOrderStatusRepository
    {
        List<OrderStatus> GetAll();
        void CreateOrderStatus(OrderStatus entity);

        bool DeleteOrderStatus(int statusId);
    }
}
