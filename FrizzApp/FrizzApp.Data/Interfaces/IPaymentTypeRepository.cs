using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FrizzApp.Data.Interfaces
{
    public interface IPaymentTypeRepository
    {
        void Create(PaymentType entity);
        List<PaymentType> GetAll();
        string Delete(PaymentTypeEnum id);
    }
}
