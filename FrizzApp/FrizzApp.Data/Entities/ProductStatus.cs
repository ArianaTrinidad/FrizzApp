using System.ComponentModel.DataAnnotations;

namespace FrizzApp.Data.Entities
{
    public class ProductStatus
    {
        private string name;

        public ProductStatusEnum ProductStatusId { get; set; }

        public string Name
        {
            get => ProductStatusId.ToString();
            set => name = ProductStatusId.ToString();
        }
    }

    public enum ProductStatusEnum
    {
        Avaiable = 1,
        WithoutStock = 2,
        Deleted = 3,
    }
}
