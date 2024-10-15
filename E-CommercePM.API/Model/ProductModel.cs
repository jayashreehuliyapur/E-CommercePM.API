using System.ComponentModel.DataAnnotations;

namespace E_CommercePM.API.Model
{
    public class ProductModel
    {

        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        
    }
}
