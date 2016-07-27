using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessCenter.Data.Models;
using ProtoBuf;

namespace BusinessCenter.Api.Models
{
    [ProtoContract]
    public class SearchViewModel
    {
        [ProtoMember(10)]
        public string RecordCount { get; set; }
        [ProtoMember(11)]
        public int NoofRecords { get; set; }
        [ProtoMember(1)]
        public int ABRAID { get; set; }
        [ProtoMember(2)]
        public string Source { get; set; }
        [ProtoMember(3)]
        public string Name { get; set; }
        [ProtoMember(4)]
        public string ABRACount { get; set; }
        [ProtoMember(5)]
        public string BBLCount { get; set; }
        [ProtoMember(6)]
        public string CBECount { get; set; }
        [ProtoMember(7)]
        public string CORPCount { get; set; }
        [ProtoMember(8)]
        public string OPLACount { get; set; }
        [ProtoMember(9)]
        public List<SearchViewModel> TotalSearchList { get; set; }
    }

    [ProtoContract]
    public class SearchInput
    {
        [ProtoMember(1)]
        public string Userid { get; set; }
        [ProtoMember(2)]
        public string SearchString { get; set; }
        [ProtoMember(3)]
        public string SearchType { get; set; }
        [ProtoMember(4)]
        public string CompanyName { get; set; }
        [ProtoMember(5)]
        public string LicenseName { get; set; }
        [ProtoMember(6)]
        public string FirstName { get; set; }
        [ProtoMember(7)]
        public string LastName { get; set; }
        [ProtoMember(8)]
        public int PageSize { get; set; }
        [ProtoMember(9)]
        public int PageIndex { get; set; }
        [ProtoMember(10)]
        public bool IsChanged { get; set; }
        [ProtoMember(11)]
        public string DisplayType { get; set; }
        [ProtoMember(12)]
        public string KeyType { get; set; }
        [ProtoMember(13)]
        public string FilterKeyword { get; set; }
    }

  
    [ProtoContract]
    public class DashBoardUserService
    {
        [ProtoMember(1)]
        public string UserId { get; set; }
    }
    public class DisplayType
    { public string DisplayValue { get; set; } }

}