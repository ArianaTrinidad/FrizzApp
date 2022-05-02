using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FrizzApp.Data.Repositories
{
    class ProductStatusRepository : IProductStatusRepository
    {
        private readonly DataContext _context;


        public ProductStatusRepository(DataContext context)
        {
            _context = context;
        }


        public virtual List<ProductStatus> GetAll()
        {
            var result = _context.ProductStatus
                .Take(5)
                .ToList();

            return result;
        }


        public void Create(ProductStatus entity)
        {
            _context.ProductStatus.Add(entity);
            _context.SaveChanges();
        }

        public string Delete(ProductStatusEnum id)
        {
            var entity = _context.ProductStatus.Where(x => x.ProductStatusId == id).FirstOrDefault();

            if (entity != null)
            {
                _context.ProductStatus.Remove(entity);
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
