using ITManagement.Models;
using System;
using System.Collections.Generic;

namespace ITManagement.ViewModels
{
    public class DeviceViewModel
    {
        public List<Categories> Category { get; set; }
        public List<Devices> Device { get; set; }
        public string FullName { get; set; }
        public int SelectedCategoryId { get; set; }
        public bool? SelectedStatus { get; set; }
        public string SelectedDeviceName { get; set; }
        public string SelectedUserName { get; set; }
    }
}
