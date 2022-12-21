using System.ComponentModel.DataAnnotations;

namespace StoresApi.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public Store? Stores { get; set; } 
         public ICollection<Product>? Products { get; set; }

       // public object Product { get; internal set; }
    }
}
