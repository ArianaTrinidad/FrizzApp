using FrizzApp.Data.Entities;
using FrizzApp.Data.Extensions;
using FrizzApp.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrizzApp.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccesor;

        public OrderRepository(DataContext context, IHttpContextAccessor httpContextAccesor)
        {
            _context = context;
            _httpContextAccesor = httpContextAccesor;
        }

        public List<Order> GetAll()
        {
            var result = _context.Orders.Include(x => x.Products).Take(50).ToList();

            return result;
        }


        public void Create(Order entity)
        {
            entity.SetCreateAuditFields(_httpContextAccesor.GetUserFromToken());

            _context.Orders.Add(entity);
            _context.SaveChanges();
        }
    }
}
