using CRUD_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRUD_MVC.Data
{
    public class createDbContext : DbContext
    {

        public createDbContext(DbContextOptions <createDbContext> options) :base(options) 
        {
            
        }


        public DbSet<Category> CategoriesDbSet { get; set; } 

        public DbSet<Post> PostDbSet { get; set; }



    }
}
