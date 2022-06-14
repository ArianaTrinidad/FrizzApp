namespace FirzzApp.Business.Dtos.RequestDto
{
    public abstract class QueryStringPaginable
    {
        private const int MAX_PAGE_SIZE = 50;
        private int _pageSize = 10;

        public int NumeroPagina { get; set; } = 1;
        public int CantidadPagina
        {
            get => _pageSize;
            set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }
    }
}