using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessCenter.Data.Models;
using BusinessCenter.Data.Model;
using BusinessCenter.Data;

namespace BusinessCenter.Api.Models
{
    public class MACAddressDetails
    {
    }
    //public class AddressDetails
    //{
    //    public string AddressID { get; set; }
    //    public string FullAddress { get; set; }
    //    public string AddressNumber { get; set; }
    //    public string AddressNumberSufix { get; set; }
    //    public string StreetName { get; set; }
    //    public string StreetType { get; set; }
    //    public string Quadrant { get; set; }
    //    public string City { get; set; }
    //    public string State { get; set; }
    //    public string Xcoord { get; set; }
    //    public string Ycoord { get; set; }
    //    public string Anc { get; set; }
    //    public string Ward { get; set; }
    //    public string Cluster { get; set; }
    //    public string ZipCode { get; set; }
    //    public string Latitude { get; set; }
    //    public string Longitude { get; set; }
    //    public string Vote_Prcnct { get; set; }
    //    public string UnitType { get; set; }
    //    public string UnitNumber { get; set; }
    //    public string Phone { get; set; }
    //    public string Email { get; set; }
    //    public string Zone { get; set; }
    //    public string SMD { get; set; }
    //}

    public class ServiceDropDownData
    {
        public List<AddressDetails> WebserviceList { get; set; }
        public List<StreetDetails> Dropdownlist { get; set; }
        public string STNAME { get; set; }

    }

    public class ServiceData
    {
        public List<CofoHopDetailsModel> WebserviceList { get; set; }
        public List<StreetDetails> Dropdownlist { get; set; }
        public string STNAME { get; set; }

    }
}