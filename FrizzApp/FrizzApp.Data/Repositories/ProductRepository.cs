using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FrizzApp.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;


        public ProductRepository(DataContext context)
        {
            _context = context;
        }


        public virtual List<Product> GetAll()
        {
            var result = _context.Products
                .Take(500)
                .ToList();

            return result;
        }


        public void Create(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public string Delete(int id)
        {
            var entity = _context.Products.Where(x => x.Id == id).FirstOrDefault();

            if (entity != null)
            {
                _context.Products.Remove(entity);
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
