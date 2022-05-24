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


        public List<Product> GetAll(int pageNumber, int pageSize, string palabraClave, decimal preciomin, decimal preciomax, int categoria)
        {
            //if (palabraClave != null || preciomin <= 0 || preciomax >= 10000 || categoria != null)
            //{
            //    var filters = _context.Products
            //      .Where(x => x.Name.Contains(palabraClave)
            //               || x.Description.Contains(palabraClave))
            //      .Where(x => x.Price > preciomin
            //               || x.Price < preciomax)
            //      .Where(x => x.CategoryId.Equals(categoria));

            //    var resultsearch = filters.ToList();

            //    return resultsearch;
            //}

            // TODO: Mejora - la lógica del paginado no va acá
            int take = pageSize > 0 ? pageSize : 100;
            int skip = pageNumber > 0 ? (pageNumber - 1) * take : 0;


            var partialResult = _context.Products
                .Where(x => x.ProductStatusId != ProductStatusEnum.Deleted)
                .ToList();

            if (string.IsNullOrEmpty(palabraClave) == false)
            {
                partialResult = partialResult
                    .Where(x => x.Name.Contains(palabraClave)
                             || x.Description.Contains(palabraClave))
                    .ToList();
            }

            if (preciomin != default)
            {
                partialResult = partialResult
                .Where(x => x.Price > preciomin) // mayor o IGUAL
                .ToList();
            }

            if (preciomax != default)
            {
                partialResult = partialResult
                .Where(x => x.Price < preciomax) // menor o IGUAL
                .ToList();
            }

            if (categoria != default)
            {
                partialResult = partialResult
                    .Where(x => x.CategoryId != categoria)
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
