using System.ComponentModel.DataAnnotations;

namespace StoresApi.Model
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public ICollection<Category>? Categories { get; set; }    



    }
}
