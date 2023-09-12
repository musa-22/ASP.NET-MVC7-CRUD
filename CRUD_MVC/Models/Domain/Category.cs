using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC.Models.Domain
{
    public class Category
    {

        public int Id { get; set; }


  
        [MaxLength(30)]
        public string category { get; set; }



        // create one to many 
        public ICollection<Post> posts { get; set;}
    }
}
