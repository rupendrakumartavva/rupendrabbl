using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
   public class SubmissionCorporationAgentAddressRepositoryData
    {
          private readonly List<SubmissionCorporation_Agent_Address> _entities;
        public bool IsInitialized;

        public void AddSubmissionCorporationAgentAddressEntity(SubmissionCorporation_Agent_Address obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionCorporation_Agent_Address> SubmissionCorporationAgentAddressEntitiesList
        {
            get { return _entities; }
        }


       public SubmissionCorporationAgentAddressRepositoryData()
       {
           IsInitialized = true;
           _entities = new List<SubmissionCorporation_Agent_Address>();
           AddSubmissionCorporationAgentAddressEntity(new SubmissionCorporation_Agent_Address()
           {
               SCAId = 1,
               SubCorporationRegId = 1,
               AddressType = "Y-CORPREG",
               FileNumber = "C943266",
              // BusinessName = "APROSERVE CORPORATION",
               //FirstName = "",
               //MiddelName = "",
               //LastName = "",
               //Address1 = "12100 Wilshire Boulevard, Suite 1400",
               //Address2 = "",
               //Address3 = "",
               //City = "Los Angeles",
               //State = "CA",
               //Country = "",
               //Telephone = "",
               //ZipCode = "90025",
               //Email = "",
               //Status = "True",
               //Quadrant = "",
               //UnitNumber = null,
               //AddressNumber = null
           });

           AddSubmissionCorporationAgentAddressEntity(new SubmissionCorporation_Agent_Address()
           {
               SCAId = 2,
               SubCorporationRegId = 1,
               AddressType = "Y-CORPAGENT",
               FileNumber = "C943266",
               BusinessName = "Auckland Park",
               FirstName = "Auckland Park",
               MiddelName = "Peter",
               LastName = "Thiel",
               Address1 = "Auckland Park",
               Address2 = "NEW YORK",
               Address3 = "AVENUE",
               City = "San Jose",
               State = "California",
               Country = "United States",
               Telephone = "123456789",
               ZipCode = "20008",
               Email = "",
               Status = "True",
               Quadrant = "SW",
               UnitNumber = "1",
               AddressNumber = "5225"
           });


           AddSubmissionCorporationAgentAddressEntity(new SubmissionCorporation_Agent_Address()
           {
               SCAId = 3,
               SubCorporationRegId = 1,
               AddressType = "NEWMAIL",
               FileNumber = "C943266",
               BusinessName = "Auckland Park",
               FirstName = "Auckland Park",
               MiddelName = "Peter",
               LastName = "Thiel",
               Address1 = "Auckland Park",
               Address2 = "NEW YORK",
               Address3 = "AVENUE",
               City = "San Jose",
               State = "California",
               Country = "United States",
               Telephone = "123456789",
               ZipCode = "20008",
               Email = "",
               Status = "True",
               Quadrant = "SW",
               UnitNumber = "1",
               AddressNumber = "5225"
           });
           AddSubmissionCorporationAgentAddressEntity(new SubmissionCorporation_Agent_Address()
           {
               SCAId = 4,
               SubCorporationRegId = 1,
               AddressType = "CORPAGENT",
               FileNumber = "C943266",
               //BusinessName = "Auckland Park",
               //FirstName = "Auckland Park",
               //MiddelName = "Peter",
               //LastName = "Thiel",
               //Address1 = "Auckland Park",
               //Address2 = "NEW YORK",
               //Address3 = "AVENUE",
               //City = "San Jose",
               //State = "California",
               //Country = "United States",
               //Telephone = "123456789",
               //ZipCode = "20008",
               //Email = "",
               //Status = "True",
               //Quadrant = "SW",
               //UnitNumber = "1",
               //AddressNumber = "5225"
           });
           //AddSubmissionCorporationAgentAddressEntity(new SubmissionCorporation_Agent_Address()
           //{
           //    SCAId =5,
           //    SubCorporationRegId = 1,
           //    AddressType = "NEWMAIL",
           //    FileNumber = "C943266",
           //    BusinessName = "Auckland Park",
           //    FirstName = "Auckland Park",
           //    MiddelName = "Peter",
           //    LastName = "Thiel",
           //    Address1 = "Auckland Park",
           //    Address2 = "NEW YORK",
           //    Address3 = "AVENUE",
           //    City = "San Jose",
           //    State = "California",
           //    Country = "United States",
           //    Telephone = "123456789",
           //    ZipCode = "20008",
           //    Email = "",
           //    Status = "True",
           //    Quadrant = "SW",
           //    UnitNumber = "1",
           //    AddressNumber = "5225"
           //});
       }

    }
}
