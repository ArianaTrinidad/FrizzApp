using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FrizzApp.Data.Repositories
{
    public class PymentTypeRepository : IPaymentTypeRepository 
    {
        private readonly DataContext _context;


        public PymentTypeRepository(DataContext context)
        {
            _context = context;
        }


        public virtual List<PaymentType> GetAll()
        {
            var result = _context.PaymentTypes
                .Take(5)
                .ToList();

            return result;
        }


        public void Create(PaymentType entity)
        {
            _context.PaymentTypes.Add(entity);
            _context.SaveChanges();
        }

        public string Delete (PaymentTypeEnum id)
        {
            var entity = _context.PaymentTypes.Where(x => x.PaymentTypeId == id).FirstOrDefault();

            if (entity != null)
            {
                _context.PaymentTypes.Remove(entity);
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
