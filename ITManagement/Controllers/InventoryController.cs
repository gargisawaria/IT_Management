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
    public class InventoryController : Controller
    {
        private readonly ITManagementContext ITM;

        public InventoryController(ITManagementContext _ITM)
        {
            ITM = _ITM;
        }
        public IActionResult DeviceFilter(int? SelectedCategoryId, bool? SelectedStatus, string SelectedDeviceName, string SelectedUserName)
        {
            var allCategories = ITM.Categories?.Distinct().ToList() ?? new List<Categories>();
            var devicesQuery = ITM.Devices.AsQueryable();

            if (SelectedCategoryId.HasValue && SelectedCategoryId > 0)
                devicesQuery = devicesQuery.Where(d => d.CategoryId == SelectedCategoryId);

            if (SelectedStatus.HasValue)
                devicesQuery = devicesQuery.Where(d => d.Status == SelectedStatus);


            if (!string.IsNullOrEmpty(SelectedUserName))
                devicesQuery = devicesQuery.Where(d => d.User.FullName.Contains(SelectedUserName, StringComparison.OrdinalIgnoreCase));
    
            if (!string.IsNullOrEmpty(SelectedDeviceName))
                devicesQuery = devicesQuery.Where(d => d.DeviceName.Contains(SelectedDeviceName, StringComparison.OrdinalIgnoreCase));

            var devices = devicesQuery
                .ToList();

            var viewModel = new DeviceViewModel
            {
                Category = allCategories,
                Device = devices
                    .Select(d => new Devices
                    {
                        Id = d.Id,
                        DeviceId = d.DeviceId,
                        DeviceName = d.DeviceName,
                        CategoryId = d.CategoryId,
                        Description = d.Description,
                        Status = d.Status,
                        UserId = d.UserId,
                        AllotedDate = d.AllotedDate,
                    })
                    .ToList(),

                SelectedCategoryId = SelectedCategoryId ?? 0,
                SelectedStatus = SelectedStatus,
                SelectedDeviceName = SelectedDeviceName,
                SelectedUserName = SelectedUserName
            };

            viewModel.Device.ForEach(device =>
            {
                if (device.UserId != null)
                {
                    device.User = new Users
                    {
                        FullName = ITM.Users.FirstOrDefault(u => u.UserId == device.UserId)?.FullName
                    };
                }

                if (device.CategoryId != 0)
                {
                    device.Category = new Categories
                    {
                        CategoryName = ITM.Categories.FirstOrDefault(c => c.CategoryId == device.CategoryId)?.CategoryName
                    };
                }
            });

            return View(viewModel);
        }

    }
}