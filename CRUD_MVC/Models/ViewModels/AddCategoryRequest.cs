using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC.Models.ViewModels
{
    public class AddCategoryRequest
    {

        [Required]
        public string category { get; set; }

    }
}
