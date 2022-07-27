using FrizzApp.Data.Entities;
using FrizzApp.Data.Enums;
using FrizzApp.Data.Extensions;
using FrizzApp.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FrizzApp.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        private readonly IHttpContextAccessor _httpContextAccesor;

        public ProductRepository(DataContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContextAccesor = httpContext;
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
                .Include(x => x.Category)
                .Skip(skip)
                .Take(take)
                .OrderBy(x => x.Id)
                .ToList();


            return result;
        }

        public Product GetById(int id)
        {
            var result = _context.Products.FirstOrDefault(x => x.Id == id);
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
            entity.SetCreateAuditFields(_httpContextAccesor.GetUserFromToken());

            _context.Products.Add(entity);
            _context.SaveChanges();
        }

       public bool ActualizePrice(int Percentage)
        {
            var entities = _context.Products;

            if (entities == null)
                return false;

            foreach (Product variable in entities)
            {
                variable.Price = variable.Price * (1 + (Percentage / 100));
            }

            _context.SaveChanges();

            return true;

        }



        public bool Update(Product entityFromDto)
        {
            var entity = _context.Products.Where(x => x.Id == entityFromDto.Id).FirstOrDefault();

            if (entity == null)
                return false;

            if (!string.IsNullOrWhiteSpace(entityFromDto.Name))
            {
                entity.Name = entityFromDto.Name;
            }

            if (!string.IsNullOrWhiteSpace(entityFromDto.Description))
            {
                entity.Description = entityFromDto.Description;
            }

            if (!string.IsNullOrWhiteSpace(entityFromDto.Notes))
            {
                entity.Notes = entityFromDto.Notes;
            }

            if (!string.IsNullOrWhiteSpace(entityFromDto.Presentation))
            {
                entity.Presentation = entityFromDto.Presentation;
            }

            if (!string.IsNullOrWhiteSpace(entityFromDto.ImageUrl))
            {
                entity.ImageUrl = entityFromDto.ImageUrl;
            }

            if (entityFromDto.Price != default)
            {
                entity.Price = entityFromDto.Price;
            }

            if (entityFromDto.IsPromo.HasValue)
            {
                entity.IsPromo = entityFromDto.IsPromo;
            }

            if (entityFromDto.Category != default)
            {
                entity.Category = entityFromDto.Category;
            }

            if (entityFromDto.ProductStatusId != default)
            {
                entity.ProductStatusId = entityFromDto.ProductStatusId;
            }

            entity.SetUpdateAuditFields(_httpContextAccesor.GetUserFromToken());

            _context.Products.Update(entity);
            _context.SaveChanges();

            return true;
        }


        public bool ChangeStatus(int id)
        {
            var entity = _context.Products.Where(x => x.Id == id).FirstOrDefault();

            if (entity != null)
            {
                entity.SetUpdateAuditFields(_httpContextAccesor.GetUserFromToken());
                entity.ProductStatusId = (int)ProductStatusEnum.WithoutStock;

                _context.Update(entity);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var entity = _context.Products.Where(x => x.Id == id).FirstOrDefault();

            if (entity != null)
            {
                entity.SetDeleteAuditFields(_httpContextAccesor.GetUserFromToken());
                entity.ProductStatusId = (int)ProductStatusEnum.Deleted;

                _context.Products.Update(entity);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
