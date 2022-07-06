using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Dtos.RequestDto
{
    public class ResponseConectionQuotationDto
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public decimal Precio { get; set; }
    }
}
