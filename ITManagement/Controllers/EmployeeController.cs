using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITManagement.Models;
using ITManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ITManagementContext ITM;

        public EmployeeController(ITManagementContext _ITM)
        {
            ITM = _ITM;
        }
        public IActionResult UsersFilter(string selectedUsername)
        {
            var usersWithDevices = ITM.Users
                .GroupJoin(
                    ITM.Devices,
                    u => u.UserId,
                    d => d.UserId,
                    (user, devices) => new
                    {
                        User = user,
                        Devices = devices
                    })
                .SelectMany(
                    result => result.Devices.DefaultIfEmpty(),
                    (result, device) => new UserWithDeviceViewModel
                    {
                        UserId = result.User.UserId,
                        UserName = result.User.FullName,
                        UserEmail = result.User.Email,
                        DeviceID = device != null ? device.DeviceId : 0,
                        DeviceName = device != null ? device.DeviceName : "N/A",
                        DeviceDescription = device != null ? device.Description : "N/A",
                        AllottedDate = device != null ? device.AllotedDate : (DateTime?)null,
                        CategoryName = device != null ? (device.Category != null ? device.Category.CategoryName : "N/A") : "N/A"
                    })
                .ToList();

            if (!string.IsNullOrEmpty(selectedUsername))
            {
                usersWithDevices = usersWithDevices.Where(u => u.UserName.Contains(selectedUsername, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var uniqueUsernames = ITM.Users.Select(u => u.FullName).Distinct().ToList();

            var viewModel = new UserWithDevicesViewModel
            {
                UsersWithDevices = usersWithDevices,
                SelectedUsername = selectedUsername,
                UniqueUsernames = uniqueUsernames
            };

            return View(viewModel);
        }


    }
}