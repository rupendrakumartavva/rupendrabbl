using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class SubmissionCofoHopeHopAddressData
    {
        private readonly List<SubmissionCofo_Hop_Ehop_Address> _entities;
        public bool IsInitialized;

        public void AddSubmissionCofoHopEhopAddressEntity(SubmissionCofo_Hop_Ehop_Address obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionCofo_Hop_Ehop_Address> SubmissionCofoHopEhopAddressList
        {
            get { return _entities; }
        }


        public SubmissionCofoHopeHopAddressData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionCofo_Hop_Ehop_Address>();

            AddSubmissionCofoHopEhopAddressEntity(new SubmissionCofo_Hop_Ehop_Address()
            {
                SubmissionCofo_Hop_Ehop_AddressId = 1,
                CustomTypeId = 1,
                CustomType = "hop",
                Name = "",
                Street = "1100 1/2 E STREET SE",
                StreetName = "E",
                StreetType = "Road",
                Quadrant = "SE",
                UnitType = "BLDG",
                Unit = "11",
                City = "WASHINGTON",
                State = "DC",
                Telephone = "1234567",
                Zip = "20003",
                IsValid = true,
                Country = "",
                AddressID = "306896",
                AddressNumber = "1100",
                AddressNumberSufix = "1/2",
                Xcoord = "400751.20",
                Ycoord = "135017.35",
                Anc = "ANC 6B",
                Ward = "Ward 6",
                Cluster = "Cluster 26",
                Latitude = "38.88299264",
                Longitude = "-76.99134209",
                Vote_Prcnct = "Precinct 91",
                CreatedDate = null,
                UpdatedDate = null
            });
            AddSubmissionCofoHopEhopAddressEntity(new SubmissionCofo_Hop_Ehop_Address()
            {
                SubmissionCofo_Hop_Ehop_AddressId = 2,
                CustomTypeId = 2,
                CustomType = "cofo",
                Name = "",
                Street = "1100 1/2 E STREET SE",
                StreetName = "E",
                StreetType = "Road",
                Quadrant = "SE",
                UnitType = "STE",
                Unit = "1111",
                City = "WASHINGTON",
                State = "DC",
                Telephone = "12345678",
                Zip = "20003",
                IsValid = true,
                Country = "",
                AddressID = "306896",
                AddressNumber = "1100",
                AddressNumberSufix = "1/2",
                Xcoord = "400751.20",
                Ycoord = "135017.35",
                Anc = "ANC 6B",
                Ward = "Ward 6",
                Cluster = "Ward 6",
                Latitude = "38.88299264",
                Longitude = "-76.99134209",
                Vote_Prcnct = "Precinct 91",
                CreatedDate = null,
                UpdatedDate = null
            });
            AddSubmissionCofoHopEhopAddressEntity(new SubmissionCofo_Hop_Ehop_Address()
            {
                SubmissionCofo_Hop_Ehop_AddressId = 3,
                CustomTypeId = 3,
                
            });
            //AddSubmissionCofoHopEhopAddressEntity(new SubmissionCofo_Hop_Ehop_Address()
            //{
            //    SubmissionCofo_Hop_Ehop_AddressId = 4,
            //    CustomTypeId = 4,
            //    CustomType = "NOPO",
            //    Name = "",
            //    Street = "staddress",
            //    StreetName = "asda",
            //    StreetType = "Avenue",
            //    Quadrant = "NW",
            //    UnitType = "BLDG",
            //    Unit = "1211",
            //    City = "city",
            //    State = "state",
            //    Telephone = "321313",
            //    Zip = "121323",
            //    IsValid = false,
            //    Country = "",
            //    AddressID = "",
            //    AddressNumber = "111",
            //    AddressNumberSufix = "1",
            //    Xcoord = "",
            //    Ycoord = "",
            //    Anc = "",
            //    Ward = "",
            //    Cluster = "",
            //    Latitude = "",
            //    Longitude = "",
            //    Vote_Prcnct = "",
            //    CreatedDate = null,
            //    UpdatedDate = null
            //});
            AddSubmissionCofoHopEhopAddressEntity(new SubmissionCofo_Hop_Ehop_Address()
            {
                SubmissionCofo_Hop_Ehop_AddressId = 5,
                CustomTypeId = 5,
                CustomType = "ehop",
                Name = "",
                Street = "1100 1/2 E STREET SE",
                StreetName = "E",
                StreetType = "Road",
                Quadrant = "SE",
                UnitType = "BLDG",
                Unit = "123",
                City = "WASHINGTON",
                State = "DC",
                Telephone = "1234",
                Zip = "20003",
                IsValid = false,
                Country = "",
                AddressID = "306896",
                AddressNumber = "1100",
                AddressNumberSufix = "1/2",
                Xcoord = "400751.20",
                Ycoord = "135017.35",
                Anc = "ANC 6B",
                Ward = "Ward 6",
                Cluster = "Ward 6",
                Latitude = "38.88299264",
                Longitude = "-76.99134209",
                Vote_Prcnct = "Precinct 91",
                CreatedDate = null,
                UpdatedDate = null
            });
        }
    }
}
