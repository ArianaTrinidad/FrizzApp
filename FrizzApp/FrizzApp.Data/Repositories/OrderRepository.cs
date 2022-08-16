using FrizzApp.Data.Entities;
using FrizzApp.Data.Extensions;
using FrizzApp.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Order>> GetAll()
        {
            var result = await _context.Orders.Include(x => x.Products).Take(50).ToListAsync();

            return result;
        }


        public async Task Create(Order entity)
        {
            entity.SetCreateAuditFields(_httpContextAccesor.GetUserFromToken());

            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();

        }
    }
}
