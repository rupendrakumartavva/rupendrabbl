using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessCenter.Data.Models;

namespace BusinessCenter.Api.Models
{
    public class SearchModelJson
    {
         public List<SearchDataViewModel> ListCounts { get; set; }
         public List<CommonData> CommonDetails { get; set; }
    }
}