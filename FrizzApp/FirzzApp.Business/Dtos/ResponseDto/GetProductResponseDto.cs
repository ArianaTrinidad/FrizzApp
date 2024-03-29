﻿namespace FirzzApp.Business.Dtos.ResponseDto
{
    public class GetProductResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Nota { get; set; }
        public string Presentacion { get; set; }
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }
        public bool EsPromo { get; set; }
        public virtual string Categoria { get; set; }
        public virtual int CategoriaId { get; set; }
        public virtual decimal PrecioDolares { get; set; }

    }
}
