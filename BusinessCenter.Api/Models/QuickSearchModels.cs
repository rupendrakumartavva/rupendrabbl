using BusinessCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessCenter.Api.Models
{
    public class SearchDataMvcViewModel
    {
        public SearchDataViewModel LicenseCounts { get; set; }
        public List<CommonData> SearchResult { get; set; }
        public SearchInput SearchData { get; set; }
        public String[] SearchCritiria { get; set; }
        public string SearchStatus { get; set; }
        public string Keyword { get; set; }
        public UserServiceModel UserServiceModel { get; set; }
    }
}