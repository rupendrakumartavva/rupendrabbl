using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class MasterRegisterAgentData
    {
        private readonly List<MasterRegisteredAgent> _entities;
        public bool IsInitialized;

        public void AddMasterRegisteredAgentEntity(MasterRegisteredAgent obj)
        {
            _entities.Add(obj);
        }

        public List<MasterRegisteredAgent> MasterRegisteredAgentEntitiesList
        {
            get { return _entities; }
        }

        public MasterRegisterAgentData()
        {
            IsInitialized = true;
            _entities = new List<MasterRegisteredAgent>();

            AddMasterRegisteredAgentEntity(new MasterRegisteredAgent()
            {
                 RegisterID =1,
                 Name = "A ABLE ACCIDENT ADVOCATE",
                 CompanyName = "A ABLE ACCIDENT ADVOCATE",
                 StreetAddress = "5225",
                 StreetNumber = "5225",
                 StreetName = "WISCONSIN",
                 StreetType = "AVENUE",
                 Quadrant = "NW",
                 UnitNumber = "1",
                 City = "WASHINGTON",
                 State = "DC",
                 Zip = "20015",
                 Telephone = "123456789",
                 FileNumber = "C943266",
                 Type =""
            });
            AddMasterRegisteredAgentEntity(new MasterRegisteredAgent()
            {
                RegisterID = 2,
                Name = "A JULIA AGENT",
                CompanyName = "A JULIA AGENT",
                StreetAddress = "5051 NEW HAMPSHIRE AVENUE NW",
                StreetNumber = "5051",
                StreetName = "NEW HAMPSHIRE",
                StreetType = "AVENUE",
                Quadrant = "NW",
                UnitNumber = "1",
                City = "WASHINGTON",
                State = "DC",
                Zip = "20011",
                Telephone = "123456789",
                FileNumber = "C262873",
                Type = ""
            });
            AddMasterRegisteredAgentEntity(new MasterRegisteredAgent()
            {
                RegisterID = 3,
                Name = "AGENT FOR SERVICE",
                CompanyName = "1100 NEW YORK AVENUE NW",
                StreetAddress = "1100 NEW YORK AVENUE NW",
                StreetNumber = "1100",
                StreetName = "NEW YORK",
                StreetType = "AVENUE",
                Quadrant = "NW",
                UnitNumber = "1",
                City = "WASHINGTON",
                State = "DC",
                Zip = "20003",
                Telephone = "123456789",
                FileNumber = "C902089",
                Type = ""
            });
            AddMasterRegisteredAgentEntity(new MasterRegisteredAgent()
            {
                RegisterID = 4,
                Name = "Victoria Hall",
                CompanyName = "Victoria Hall",
                StreetAddress = "Victoria Hall",
                StreetNumber = "48",
                StreetName = "NEW YORK",
                StreetType = "STREET",
                Quadrant = "NW",
                UnitNumber = "1",
                City = "Fort Worth",
                State = "Texas",
                Zip = "20003",
                Telephone = "123456789",
                FileNumber = "C904040",
                Type = ""
            });
        }
    }
}