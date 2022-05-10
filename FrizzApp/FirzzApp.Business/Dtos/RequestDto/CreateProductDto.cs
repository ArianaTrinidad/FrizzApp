namespace FirzzApp.Business.Dtos.RequestDto
{
    public class CreateProductDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Nota { get; set; }
        public string Presentacion { get; set; }
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }
        public bool EsPromo { get; set; }
        public int? Categoria { get; set; }
    }
}
