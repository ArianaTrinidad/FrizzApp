using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Interfaces
{
    public interface IConvertApiConection
    {
        public Task<decimal> GetDollarAsync();
    }
}
