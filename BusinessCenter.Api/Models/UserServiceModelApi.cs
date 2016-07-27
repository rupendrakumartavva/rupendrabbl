using System;

namespace BusinessCenter.Api.Models
{
    public class UserServiceModelApi
    {
        public int ServiceId { get; set; }
        public string DataSource { get; set; }
        public int EntityId { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }
        public int UserId { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }

        public string DisplayType { get; set; }


        public string CompanyName { get; set; }

        public string LicenseNumber { get; set; }
    }
}