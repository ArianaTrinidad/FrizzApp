using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrizzApp.Data.Repositories
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly DataContext _context;


        public OrderStatusRepository(DataContext context)
        {
            _context = context;
        }

        public List<OrderStatus> GetAll()
        {
            var result = _context.OrderStatus
                .Take(500)
                .ToList();

            return result;
        }


        public void CreateOrderStatus(OrderStatus entity)
        {
            _context.OrderStatus.Add(entity);
            _context.SaveChanges();
        }


        public string DeleteOrderStatus(OrderStatusEnum statusId)
        {
            var entity = _context.OrderStatus.Where(x => x.OrderStatusId == statusId).FirstOrDefault();

            if (entity != null)
            {
                _context.OrderStatus.Remove(entity);
                _context.SaveChanges();

                return "Entity deleted correctly";
            }
            else
            {
                return "The entity does not exists";
            }
        }
    }
}
