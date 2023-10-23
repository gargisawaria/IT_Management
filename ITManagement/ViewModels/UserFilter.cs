using ITManagement.Models;
using System;
using System.Collections.Generic;

namespace ITManagement.ViewModels
{
    public class UserWithDevicesViewModel
    {
        public string SelectedUsername { get; set; }
        public List<string> UniqueUsernames { get; set; }
        public List<UserWithDeviceViewModel> UsersWithDevices { get; set; }
    }

    public class UserWithDeviceViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string DeviceName { get; set; }
        public int DeviceID { get; set; }
        public string DeviceDescription { get; set; }
        public DateTime? AllottedDate { get; set; }
        public string CategoryName { get; set; }
    }
}
