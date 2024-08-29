using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Category
    {
        [Key]
        [Range(1, 100, ErrorMessage ="Id must be between 1-100")]
        [DisplayName("Category ID")]
        public int Id { get; set; }


        [DisplayName("Category Name")]
        [MaxLength(30)]
        public required string Name { get; set; }
    }
}
