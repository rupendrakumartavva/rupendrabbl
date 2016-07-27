using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Common
{
    /// <summary>
    ///
    /// </summary>
    public class SearchServiceInput
    {
        public string SearchString { get; set; }
        public string SearchType { get; set; }
        public string CompanyName { get; set; }
        public string LicenseName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Userid { get; set; }
        public bool IsChanged { get; set; }
        public string DisplayType { get; set; }
        public string KeyType { get; set; }
    }
}