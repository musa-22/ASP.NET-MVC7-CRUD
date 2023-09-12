using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC.Models.ViewModels
{
    public class EditCategoryRequest
    {


        public int Id { get; set; }

        [Required]
        public string category { get; set; }


    }
}
