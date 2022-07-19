using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Dtos.RequestDto
{
    public class FileUploadViewModel
    {
        public string Name { get; set; }
        public IFormFile Excel { get; set; }
    }
}
