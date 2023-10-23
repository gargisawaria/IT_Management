using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITManagement.Models;

using Microsoft.AspNetCore.Mvc;

namespace ITManagement.Controllers
{
    public class ManageCategoryController : Controller
    {
        private readonly ITManagementContext ITM;

        public ManageCategoryController(ITManagementContext _ITM)
        {
            ITM = _ITM;
        }


        public IActionResult getCategory()
        {
            List<Categories> obj = ITM.Categories.ToList();
            ViewBag.categories = obj;
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Categories Category)
        {



            var CategoryEntity = new Categories
            {

                CategoryName = Category.CategoryName,

            };

            ITM.Categories.Add(CategoryEntity);
            ITM.SaveChanges();
            return RedirectToAction("getCategory");

        }


        [HttpPost]
        public IActionResult DeleteCategory(int deleteCategoryId)
        {
            var Cat = ITM.Categories.FirstOrDefault(ca => ca.CategoryId == deleteCategoryId);

            var device = ITM.Devices.ToList().Where(d => d.CategoryId == deleteCategoryId);

            //if(device.Count() > 0)
            //{
            //    string alertMessage = "This is an alert from the controller!";
            //    string script = $@"<script>alert('{alertMessage}');</script>";
            //    return RedirectToAction("getCategory");
            //}
            if (device.Count() > 0)
            {
                TempData["DisplayAlert"] = true;
                return RedirectToAction("getCategory");
            }


            // Remove the device from the context and save changes.
            ITM.Categories.Remove(Cat);
            ITM.SaveChanges();

            return RedirectToAction("getCategory");
        }


        public IActionResult UpdateCategory(int id)
        {
            var data = ITM.Categories.Where(x => x.CategoryId == id).FirstOrDefault();

            return View(data);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = ITM.Categories.Where(x => x.CategoryId == id).FirstOrDefault();



            return View(data);



        }


        [HttpPost]
        public IActionResult Edit(Categories Model)
        {
            // Find the category by ID
            var data = ITM.Categories.Where(x => x.CategoryId == Model.CategoryId).FirstOrDefault();



            if (data != null)
            {
                data.CategoryName = Model.CategoryName;
                ITM.SaveChanges();
            }
            return RedirectToAction("getCategory");
        }
    }
}