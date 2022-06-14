using FrizzApp.Data.Entities;
using FrizzApp.Data.Enums;
using FrizzApp.Data.Extensions;
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
            int skip = pageNumber > 0 ? (pageNumber - 1) * (pageSize > 0 ? pageSize : 100) : 0;


            var partialResult = _context.Products
                .Where(x => x.ProductStatusId != (int)ProductStatusEnum.Deleted);

            if (string.IsNullOrWhiteSpace(palabraClave) == false)
            {
                partialResult = partialResult
                    .Where(x => x.Name.Contains(palabraClave)
                             || x.Description.Contains(palabraClave));
            }

            if (precioMin != default)
            {
                partialResult = partialResult
                    .Where(x => x.Price >= precioMin);
            }

            if (precioMax != default)
            {
                partialResult = partialResult
                    .Where(x => x.Price <= precioMax);
            }

            if (categoriaId != default)
            {
                partialResult = partialResult
                    .Where(x => x.CategoryId != categoriaId);
            }


            var result = partialResult
                .Skip(skip)
                .Take(take)
                .OrderBy(x => x.Id)
                .ToList();


            return result;
        }


        public List<Product> GetAllQueryableExtesion(int pageNumber, int pageSize, string palabraClave, decimal precioMin, decimal precioMax, int categoriaId)
        {
            int take = pageSize > 0 ? pageSize : 100;
            int skip = pageNumber > 0 ? (pageNumber - 1) * (pageSize > 0 ? pageSize : 100) : 0;

            return _context.Products
                .Where(x => x.ProductStatusId != (int)ProductStatusEnum.Deleted)
                //.WhereIf( condition: , func: lambda expression )
                .WhereIf(string.IsNullOrWhiteSpace(palabraClave) == false, x => x.Name.Contains(palabraClave) || x.Description.Contains(palabraClave))
                .WhereIf(precioMin != default, x => x.Price >= precioMin)
                .WhereIf(precioMax != default, x => x.Price <= precioMax)
                .WhereIf(categoriaId != default, x => x.CategoryId != categoriaId)
                .Skip(skip)
                .Take(take)
                .OrderBy(x => x.Id)
                .ToList();
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
                entity.ProductStatusId = (int)ProductStatusEnum.Deleted;
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
