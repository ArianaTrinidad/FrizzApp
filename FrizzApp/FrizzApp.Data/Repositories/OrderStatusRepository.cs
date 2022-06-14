using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            var result = _context.OrderStates
                .Take(500)
                .ToList();

            return result;
        }


        public void CreateOrderStatus(OrderStatus entity)
        {
            _context.OrderStates.Add(entity);
            _context.SaveChanges();
        }


        public string DeleteOrderStatus(int statusId)
        {
            var entity = _context.OrderStates.Where(x => x.OrderStatusId == statusId).FirstOrDefault();

            if (entity != null)
            {
                _context.OrderStates.Remove(entity);
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
