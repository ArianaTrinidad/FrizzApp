using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FrizzApp.Data.Interfaces
{
    public interface IPaymentTypeRepository
    {
        List<PaymentType> GetAll();
        void Create(PaymentType entity);
        bool Delete(int id);
    }
}
