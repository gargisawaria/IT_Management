using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITManagement.Controllers
{
    public class ManageUsersController : Controller
    {

        private readonly ITManagementContext ITM;

        public ManageUsersController(ITManagementContext _ITM)
        {
            ITM = _ITM;
        }
        // GET: /<controller>/


        public IActionResult getUser()
        {
            List<Users> obj = ITM.Users.ToList();
            ViewBag.user = obj;
            return View();
        }



        [HttpPost]
        public IActionResult CreateUser(Users UserModel)
        {


            string defaultCreatedBy = "admin";
            string defaultUpdatedBy = "admin";
            DateTime? defaultUpdatedAt = null;
            DateTime currentDateTime = DateTime.Now;
            // Map the DeviceModel to your actual Devices entity and set the navigation properties.
            var UserEntity = new Users
            {
                UserId = UserModel.UserId,
                FullName = UserModel.FullName,
                Email = UserModel.Email,
                CreatedBy = defaultCreatedBy,
                UpdatedBy = defaultUpdatedBy,
                UpdatedAt = defaultUpdatedAt,
                CreatedAt = currentDateTime,

            };

            ITM.Users.Add(UserEntity);
            ITM.SaveChanges();
            return RedirectToAction("getUser");

        }


        [HttpPost]
        public IActionResult UpdateUser([FromBody] Users updatedUser)
        {
            if (ModelState.IsValid)
            {
                // Find the existing Device entity by its DeviceID.
                var existingUser = ITM.Users.FirstOrDefault(U => U.UserId == updatedUser.UserId);

                if (existingUser == null)
                {
                    return NotFound("User not found");
                }

                string defaultUpdatedBy = "admin";
                string defaultCreatedBy = "admin";
                DateTime? defaultUpdatedAt = null;
                DateTime currentDateTime = DateTime.Now;


                // Update the properties of the existing Device entity with the values from the updatedDevice.
                existingUser.FullName = updatedUser.FullName;
                existingUser.Email = updatedUser.Email;
                existingUser.CreatedBy = defaultCreatedBy;
                existingUser.UpdatedBy = defaultUpdatedBy;
                existingUser.UpdatedAt = defaultUpdatedAt;
                existingUser.CreatedAt = currentDateTime;

                // Save the changes to the database.
                ITM.SaveChanges();

                return Ok("User updated successfully");
            }

            // If the ModelState is not valid, return to handle validation errors.
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IActionResult DeleteUser(string deleteUserId)
        {
            var Use = ITM.Users.FirstOrDefault(u => u.UserId == deleteUserId);
            var device = ITM.Devices.ToList().Where(d => d.UserId == deleteUserId);
            if (device.Count() > 0)
            {
                TempData["DisplayAlert"] = true;
                return RedirectToAction("getUser");
            }
            // Remove the device from the context and save changes.
            ITM.Users.Remove(Use);
            ITM.SaveChanges();

            return RedirectToAction("getUser");
        }


        [HttpGet]
        public IActionResult Edit(string userId)
        {
            var data = ITM.Users.Where(x => x.UserId == userId).FirstOrDefault();

            return View(data);
        }


        [HttpPost]
        public IActionResult Edit(Users Model)
        {
            // Find the category by ID
            var data = ITM.Users.FirstOrDefault(x => x.UserId == Model.UserId);

            if (data.FullName!=null && data.Email!=null)
            {
                //data.UserId = Model.UserId;
                data.FullName = Model.FullName;
                data.Email = Model.Email;
                ITM.SaveChanges();
            }
            return RedirectToAction("getUser");
        }
    }
}