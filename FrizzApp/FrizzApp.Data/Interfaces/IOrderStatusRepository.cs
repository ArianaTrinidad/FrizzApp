using FrizzApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrizzApp.Data.Interfaces
{
    public interface IOrderStatusRepository
    {
        void CreateOrderStatus(OrderStatus entity);
        List<OrderStatus> GetAll();

        string DeleteOrderStatus(OrderStatusEnum stateId);
    }
}
