using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC.Models.ViewModels
{
    public class DeletePostRequest
    {



        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? imageUrl { get; set; }

        [Required]
        public string Description { get; set; }



    }
}
