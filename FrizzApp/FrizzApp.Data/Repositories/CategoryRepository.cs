using FrizzApp.Data.Entities;
using FrizzApp.Data.Extensions;
using FrizzApp.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace FrizzApp.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccesor;

        public CategoryRepository(DataContext context, IHttpContextAccessor httpContextAccesor)
        {
            _context = context;
            _httpContextAccesor = httpContextAccesor;
        }

        public List<Category> GetAll()
        {
            var result = _context.Categories
                .Take(500)
                .ToList();

            return result;
        }


        public void CreateCategory(Category entity)
        {
            entity.SetCreateAuditFields(_httpContextAccesor.GetUserFromToken());

            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public bool DeleteCategory(int id)
        {
            var entity = _context.Categories.Where(x => x.CategoryId == id).FirstOrDefault();

            if (entity != null)
            {
                entity.SetDeleteAuditFields(_httpContextAccesor.GetUserFromToken());

                _context.Categories.Remove(entity);
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