using FrizzApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrizzApp.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();
        Task Create(Order entity);
    }
}
