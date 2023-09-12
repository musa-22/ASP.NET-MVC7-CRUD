using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC.Models.Domain
{
    public class Post
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string? imageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime PublishedDate { get; set; }

        // ctrate 1 to many relation 
        public ICollection<Category> Categories { get; set;}




    }
}
