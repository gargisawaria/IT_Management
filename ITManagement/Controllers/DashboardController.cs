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
    public class DashboardController : Controller
    {
        private readonly ITManagementContext ITM;

        public DashboardController(ITManagementContext _ITM)
        {
            ITM = _ITM;
        }

        public IActionResult Dashboard()
        {
            var viewModel = new DashboardViewModel
            {
                NumberOfDevices = ITM.Devices.Count(),
                NumberOfUsers = ITM.Users.Count(),
                NumberOfAllottedDevices = ITM.Devices.Count(d => d.Status == true),
                BarChartData = ITM.Categories.Select(c => new BarChartData
                {
                    Category = c.CategoryName,
                    NumberOfUsers = c.Devices.Count()
                }).ToList(),
                PieChartData = new PieChartData
                {
                    NumberOfAllottedDevices = ITM.Devices.Count(d => d.Status == true),
                    NumberOfNotAllottedDevices = ITM.Devices.Count(d => d.Status == false)
                }
            };

            return View(viewModel);
        }
    }
}