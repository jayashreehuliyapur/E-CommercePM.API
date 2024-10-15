using System.ComponentModel.DataAnnotations;

namespace E_CommercePM.API.Model
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

       
    }
}
