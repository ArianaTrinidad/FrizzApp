using FrizzApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Dtos.RequestDto
{
   public class ChangeStockStatusDto
    {
        public int Id { get; set; }
        public ProductStatusEnum estado { get; set; }
    }
}
