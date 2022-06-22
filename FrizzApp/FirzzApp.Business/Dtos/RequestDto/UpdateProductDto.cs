﻿using FrizzApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Dtos.RequestDto
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Nota { get; set; }
        public string Presentacion { get; set; }
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }
        public bool EsPromo { get; set; }
        public int? Categoria { get; set; }
        public ProductStatusEnum EstadoProductoId { get; set; }

    }
}
