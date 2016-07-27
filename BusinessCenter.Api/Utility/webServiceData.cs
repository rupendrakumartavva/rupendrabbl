using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessCenter.Api.BusinessLocation;
using BusinessCenter.Api.Models;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Api.Utility
{
    public class WebServiceData : IwebServiceData
    {
        public WebServiceData()
        {
            
        }

        public List<AddressDetails> Data(string searchType)
        {
            var addetails = new List<AddressDetails>();
            try
            {

            
                var getLocationDetails = new LocationVerifierSoapClient();
                var getData = getLocationDetails.getDCAddresses2(searchType, "json");
                if (getData.returnDataset != null)
                {
                    var addresslist = getData.returnDataset.Tables[0].Rows;
                    return (from object addressData in addresslist
                        select new AddressDetails
                        {
                            AddressID = ((System.Data.DataRow) (addressData)).ItemArray[0].ToString(),
                            AddressNumber = ((System.Data.DataRow) (addressData)).ItemArray[3].ToString(),
                            FullAddress = ((System.Data.DataRow) (addressData)).ItemArray[2].ToString(),
                            AddressNumberSufix = ((System.Data.DataRow) (addressData)).ItemArray[4].ToString(),
                            StreetName = ((System.Data.DataRow) (addressData)).ItemArray[5].ToString(),
                            StreetType = ((System.Data.DataRow) (addressData)).ItemArray[6].ToString(),
                            Quadrant = ((System.Data.DataRow) (addressData)).ItemArray[7].ToString(),
                            City = ((System.Data.DataRow) (addressData)).ItemArray[8].ToString(),
                            State = ((System.Data.DataRow) (addressData)).ItemArray[9].ToString(),
                            Xcoord = ((System.Data.DataRow) (addressData)).ItemArray[10].ToString(),
                            Ycoord = ((System.Data.DataRow) (addressData)).ItemArray[11].ToString(),
                            Anc = ((System.Data.DataRow) (addressData)).ItemArray[13].ToString(),
                            Ward = ((System.Data.DataRow) (addressData)).ItemArray[15].ToString(),
                            Cluster = ((System.Data.DataRow) (addressData)).ItemArray[17].ToString(),
                            ZipCode = ((System.Data.DataRow) (addressData)).ItemArray[23].ToString(),
                            Latitude = ((System.Data.DataRow) (addressData)).ItemArray[31].ToString(),
                            Longitude = ((System.Data.DataRow) (addressData)).ItemArray[32].ToString(),
                            Vote_Prcnct = ((System.Data.DataRow) (addressData)).ItemArray[21].ToString()
                            //STNAME = ((System.Data.DataRow)(ddd)).ItemArray[5].ToString(),
                            //STREET_TYPE = ((System.Data.DataRow)(ddd)).ItemArray[6].ToString(),
                            //QUADRANT = ((System.Data.DataRow)(ddd)).ItemArray[7].ToString(),
                            //CITY = ((System.Data.DataRow)(ddd)).ItemArray[8].ToString(),
                            //STATE = ((System.Data.DataRow)(ddd)).ItemArray[9].ToString(),
                            //ZIPCODE = ((System.Data.DataRow)(ddd)).ItemArray[23].ToString(),
                            //FullAddress = ((System.Data.DataRow)(ddd)).ItemArray[2].ToString()
                        }).ToList();
                }
             
            }
            catch (Exception ex)

            {
                throw ex;
            }
            return addetails;
        }
    
}
}