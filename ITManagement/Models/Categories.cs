using System;
using System.Collections.Generic;

namespace ITManagement.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Devices = new HashSet<Devices>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Devices> Devices { get; set; }
    }
}
