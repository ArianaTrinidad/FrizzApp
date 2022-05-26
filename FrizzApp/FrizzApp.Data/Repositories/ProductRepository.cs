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


        public List<Product> GetAll(string search, int? category, decimal? priceMin, decimal? priceMax,
                                    int pageNumber, int pageSize)
        {
            // TODO: Mejora - la lógica del paginado no va acá
            int take = pageSize > 0 ? pageSize : 100;
            int skip = pageNumber > 0 ? (pageNumber - 1) * take : 0;

            var partialResult = _context.Products
                .Where(x => x.ProductStatusId != ProductStatusEnum.Deleted)
                .ToList();

            if (string.IsNullOrEmpty(search) == false)
            {
                partialResult = partialResult
                    .Where(x => x.Name.Contains(search)
                             || x.Description.Contains(search))
                    .ToList();
            }

            if (priceMin != default || priceMax != default) 
            //en la otra rama están separados (priceMin != null || priceMax != null)
            {
                partialResult = partialResult
                .Where(x => x.Price >= priceMin
                         || x.Price <= priceMax)
                .ToList();
            }


            if (category != default) // (category != null)
            {
                partialResult = partialResult
                    .Where(x => x.CategoryId == category) //equals
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
