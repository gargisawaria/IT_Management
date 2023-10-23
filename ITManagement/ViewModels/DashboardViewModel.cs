// DashboardViewModel.cs
using System.Collections.Generic;

namespace ITManagement.ViewModels
{
    public class DashboardViewModel
    {
        public int NumberOfDevices { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfAllottedDevices { get; set; }
        public List<BarChartData> BarChartData { get; set; }
        public PieChartData PieChartData { get; set; }
    }

    public class BarChartData
    {
        public string Category { get; set; }
        public int NumberOfUsers { get; set; }
    }

    public class PieChartData
    {
        public int NumberOfAllottedDevices { get; set; }
        public int NumberOfNotAllottedDevices { get; set; }
    }
}
