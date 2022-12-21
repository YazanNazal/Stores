using System.ComponentModel.DataAnnotations;

namespace StoresApi.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
         //  public object Categories { get; internal set; }
        public ICollection<Category>? Categories { get; set; } 
         // public Category category { get; set; }  


    }
}
