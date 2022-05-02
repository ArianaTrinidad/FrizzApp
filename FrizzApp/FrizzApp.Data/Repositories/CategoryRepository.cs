using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrizzApp.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;


        public CategoryRepository(DataContext context)
        {
            _context = context;
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
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public string DeleteCategory(int id)
        {
            var entity = _context.Categories.Where(x => x.CategoryId == id).FirstOrDefault();

            if (entity != null)
            {
                _context.Categories.Remove(entity);
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