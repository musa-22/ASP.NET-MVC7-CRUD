using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC.Models.ViewModels
{
    public class EditPostRequest
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? imageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime PublishedDate { get; set; }



        [Required]
        //SelectListItem is a class and is used to create a dropdown in asp.net core.
        public IEnumerable<SelectListItem> displayCategoryHelper { get; set; }



        public string[] SelectCaregory { get; set; } = Array.Empty<string>();





    }
}
