using System;
using System.Collections.Generic;

namespace ITManagement.Models
{
    public partial class Devices
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
        public DateTime? AllotedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public Categories Category { get; set; }
        public Users User { get; set; }
    }
}
