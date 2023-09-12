using CRUD_MVC.Data;
using CRUD_MVC.Models.Domain;
using CRUD_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CRUD_MVC.Controllers
{
    public class CategoryController : Controller
    {


        private readonly createDbContext _db;

        public CategoryController(createDbContext createDbContext)
        {
            this._db = createDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {

            var DataList = await _db.CategoriesDbSet.ToListAsync();

            return View(DataList);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryRequest addCategoryReq)
        {

            if(ModelState.IsValid)
            {

                // map data from category Model 

                Category categoryAdd = new()
                {

                    category = addCategoryReq.category

                };

                if (categoryAdd != null)
                {

                    await _db.AddAsync(categoryAdd);

                    await _db.SaveChangesAsync();
                    TempData["success"] = "New Category has been created successfuly ";

                }

                return RedirectToAction("List");

            }

              
            return View();
        }





        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var editData = await _db.CategoriesDbSet.FindAsync(id);     

            return View(editData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryRequest editCategoryReq)
        {

            if (ModelState.IsValid)
            {

                Category edit = new()
                {
                    Id = editCategoryReq.Id,
                    category = editCategoryReq.category

                };



                var editData = await _db.CategoriesDbSet.FindAsync(edit.Id);

                if (editData != null)
                {

                    editData.category = editCategoryReq.category;

                    await _db.SaveChangesAsync();
                    TempData["success"] = "The category has been edited successfully ";
                };


                return RedirectToAction("List");




            }


            return View();
        }





        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var deleteData = await _db.CategoriesDbSet.FindAsync(id);

            
            return View(deleteData);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(EditCategoryRequest deleteCat)
        {

            var deleteData = await _db.CategoriesDbSet.FindAsync(deleteCat.Id);

            if (deleteData != null)
            {

                  _db.Remove(deleteData);

                await _db.SaveChangesAsync();
                TempData["success"] = " The category has been deleted successfully ";

                return RedirectToAction("List");
            }

            return View(null);
        }

        








    }
}
