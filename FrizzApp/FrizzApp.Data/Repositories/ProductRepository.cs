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


        public List<Product> GetAll(int pageNumber, int pageSize, string palabraClave, decimal precioMin, decimal precioMax, int categoriaId)
        {
            int take = pageSize > 0 ? pageSize : 100;
            int skip = pageNumber > 0 ? (pageNumber - 1) * take : 0;


            var partialResult = _context.Products
                .Where(x => x.ProductStatusId != ProductStatusEnum.Deleted)
                .ToList();


            if (string.IsNullOrWhiteSpace(palabraClave) == false)
            {
                partialResult = partialResult
                    .Where(x => x.Name.Contains(palabraClave)
                             || x.Description.Contains(palabraClave))
                    .ToList();
            }

            if (precioMin != default)
            {
                partialResult = partialResult
                    .Where(x => x.Price >= precioMin)
                    .ToList();
            }

            if (precioMax != default)
            {
                partialResult = partialResult
                    .Where(x => x.Price <= precioMax)
                    .ToList();
            }

            if (categoriaId != default)
            {
                partialResult = partialResult
                    .Where(x => x.CategoryId != categoriaId)
                    .ToList();
            }


            var result = partialResult
                .Skip(skip)
                .Take(take)
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
                entity.ProductStatusId = ProductStatusEnum.Deleted;
                _context.Products.Update(entity);
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
