
using CRUD_MVC.Data;
using CRUD_MVC.Models.Domain;
using CRUD_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRUD_MVC.Controllers
{
    public class PostsController : Controller
    {
        private readonly createDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public PostsController(createDbContext createDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this._db = createDbContext;
            this._webHostEnvironment = webHostEnvironment;
        }


        /*
         *  get all list of data 
         */
        [HttpGet]
        public async Task<IActionResult> List()
        {

            var getList = await _db.PostDbSet.ToListAsync();

            return View(getList);
        }

        /*
         *  get the the category data 
         */

        [HttpGet] 
        public async Task<IActionResult> Add()
        {

            if (ModelState.IsValid)
            {


                var getCategory = await _db.CategoriesDbSet.ToListAsync();


                AddPostRequest model = new()
                {

                    displayCategoryHelper = getCategory.Select( x => new SelectListItem {
                        
                        Text = x.category, Value = x.Id.ToString() 
                    }

                 )

                };

                

                    return View(model);

                



            }
            return View("Add");

        }

        /*
         * implement add post functions 
         * 
         *create folder in the wwwroot folder and name it Images
         *
         *use IFormFile interface to transimtt files/ images 
         * 
         * identify the path for where to save the images .
         *  
         *  ineject IWebHostEnvironment interface into the controller and assigned to the private property to get the wepRootPath 
         *  
         */

        [HttpPost]
        public async Task<IActionResult> Add(AddPostRequest addPostReq, IFormFile formFile)
        {


            if (!ModelState.IsValid)
            {

               
                string wwwRootPath = _webHostEnvironment.WebRootPath;


                if (formFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);

                    addPostReq.imageUrl = fileName;

                    string postPath = Path.Combine(wwwRootPath + "/Images", fileName);


                    using (var fileStream = new FileStream(postPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(fileStream);
                    }

                    Post addPost = new()
                    {
                        Title = addPostReq.Title,
                        imageUrl = addPostReq.imageUrl,
                        Description = addPostReq.Description,
                        PublishedDate = DateTime.Now,

                    };




                    // Use a dictionary to store selected categories
                    var selectCategory = new Dictionary<int, Category>();

                    // loob throgh array to get category 
                    foreach (var getId in addPostReq.SelectCaregory)
                    {
                        int getCategoryId = int.Parse(getId);
                        var getExistCategory = await _db.CategoriesDbSet.FindAsync(getCategoryId);

                        if (getExistCategory != null)
                        {
                            selectCategory[getCategoryId] = getExistCategory;
                        }
                    }
                    addPost.Categories = selectCategory.Values.ToList();



                    var addNewPost = await _db.AddAsync(addPost);

                    if (addNewPost != null)
                    {
                        await _db.SaveChangesAsync();
                        TempData["success"] = "New Category has been created successfuly ";
                        return RedirectToAction("Add");
                    }

                }


            }


            return View("Add");
        }



        /*
         *  get targeted data to edit by id.
         */

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var getData = await _db.PostDbSet.FindAsync(id);

            return View(getData);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(EditPostRequest editPost, IFormFile formFile)
        {

            if (!ModelState.IsValid)
            {

                var getDataEdit = await _db.PostDbSet.FindAsync(editPost.Id);

               
                string wwwRootPath = _webHostEnvironment.WebRootPath;

               
                if(formFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    
                    string postPath = Path.Combine(wwwRootPath + "/Images", fileName);

                    using(var fileStream = new FileStream(postPath, FileMode.Create))
                    {

                       await formFile.CopyToAsync(fileStream);

                    }

                    getDataEdit.Title = editPost.Title;

                    getDataEdit.Description = editPost.Description;

                    getDataEdit.imageUrl = fileName;

                }
                else
                {

                    getDataEdit.Title =editPost.Title;
                    getDataEdit.Description = editPost.Description;
                 

                }
                await _db.SaveChangesAsync();

                return RedirectToAction("List");


            }

            return View();
        }




        /*
         *  get targeted data to delete it by id.
         */

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var getData = await _db.PostDbSet.FindAsync(id);

            return View(getData);
        }





        [HttpPost]
        public async Task<IActionResult> Delete(DeletePostRequest delete)
        {
            
           

                var getDataToDelete = await _db.PostDbSet.FindAsync(delete.Id);

                if (getDataToDelete != null)
                {

                    _db.Remove(getDataToDelete);

                 await   _db.SaveChangesAsync();
                TempData["success"] = " The Post has been deleted successfully ";

                return RedirectToAction("List");
            }






            return View(null);
        }

    }
}
