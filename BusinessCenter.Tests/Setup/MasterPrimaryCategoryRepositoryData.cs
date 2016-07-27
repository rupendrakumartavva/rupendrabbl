using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class MasterPrimaryCategoryRepositoryData
    {
        private readonly List<MasterPrimaryCategory> _entities;
        public bool IsInitialized;

        public void AddMasterPrimaryCategoryEntity(MasterPrimaryCategory obj)
        {
            _entities.Add(obj);
        }

        public IEnumerable<MasterPrimaryCategory> MasterPrimaryCategoryEntitiesList
        {
            get { return _entities; }
        }

        public MasterPrimaryCategoryRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<MasterPrimaryCategory>();

            AddMasterPrimaryCategoryEntity(new MasterPrimaryCategory()
            {
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
                Description = "Hotel",
                Endorsement="Housing: Transient",
                CategoryCode = "5107",
                UnitOne="Seats",
                UnitTwo=null,
                App_Type = "B",
                IsSecondaryLicenseCategory=true,
                IsSubCategory=false,
                Status = true
            });
            AddMasterPrimaryCategoryEntity(new MasterPrimaryCategory()
            {
                PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",
                ActivityID = "BE29F663-A3FA-4B64-8697-C9C4FF91B69F",
                Description = "General Business",
                Endorsement = "General Business",
                CategoryCode = "4001",
                UnitOne="NA",
                UnitTwo = "NA",
                App_Type = "B",
                IsSecondaryLicenseCategory=true,
                IsSubCategory=false,
                Status = true
            });

              AddMasterPrimaryCategoryEntity(new MasterPrimaryCategory()
            {
                PrimaryID = "16FD71A3-7DD7-4B65-A006-777691BCB7FC",
                ActivityID = "668F4B68-8437-431F-9052-60809E556028",
                Description = "chartiable",
                Endorsement = "Inspected Sales and Services",
                CategoryCode = "6005",
                UnitOne = "NA",
                UnitTwo="NA",
                App_Type = "I",
                IsSecondaryLicenseCategory=true,
                IsSubCategory=false,
                Status = true
            });


              AddMasterPrimaryCategoryEntity(new MasterPrimaryCategory()
              {
                  PrimaryID = "BBD6F69D-A82F-4A46-AC21-0E08E29FD600",
                  ActivityID = "C49EACE2-2180-430E-BA2F-8B891D659421",
                  Description = "ONE FAMILY RENTAL",
                  Endorsement = "General Service and Repair",
                  CategoryCode = "4202",
                  UnitOne = "NA",
                  UnitTwo = "NA",
                  App_Type = "B",
                  IsSecondaryLicenseCategory = true,
                  IsSubCategory = false,
                  Status = true
              });

              AddMasterPrimaryCategoryEntity(new MasterPrimaryCategory()
              {
                  PrimaryID = "796B2CFD-EBDC-4480-B45C-502A816860FB",
                  ActivityID = "1ED33685-B54E-4D7A-BBCE-1C93BD762CB1",
                  Description = "Cigarette Retail",
                  Endorsement = "General Sales",
                  CategoryCode = "4103",
                  UnitOne = "NA",
                  UnitTwo = "NA",
                  App_Type = "B",
                  IsSecondaryLicenseCategory = true,
                  IsSubCategory = false,
                  Status = true
              });

              AddMasterPrimaryCategoryEntity(new MasterPrimaryCategory()
              {
                  PrimaryID = "640C7E4B-8F90-4E59-9BA5-D1EA71E7B6FB",
                  ActivityID = "FF0C5A9C-86B4-4378-BA8F-C1CE165B814E",
                  Description = "Food Vending Machine",
                  Endorsement = "Public Health: Food",
                  CategoryCode = "9307",
                  UnitOne = "Machines",
                  UnitTwo = "NA",
                  App_Type = "B",
                  IsSecondaryLicenseCategory = true,
                  IsSubCategory = false,
                  Status = true
              });
              AddMasterPrimaryCategoryEntity(new MasterPrimaryCategory()
              {
                  PrimaryID = "796B2CFD-EBDC-4480-B45C-502A81686999",
                  ActivityID = "FF0C5A9C-86B4-4378-BA8F-C1CE165B814E",
                  Description = "NA",
                  Endorsement = "Public Health: Food",
                  CategoryCode = "9880",
                  UnitOne = "Machines",
                  UnitTwo = "NA",
                  App_Type = "B",
                  IsSecondaryLicenseCategory = true,
                  IsSubCategory = false,
                  Status = true
              });

              AddMasterPrimaryCategoryEntity(new MasterPrimaryCategory()
              {
                  PrimaryID = "640C7E4B-8F90-4E59-9BA5-D1EA71E7BSEA",
                  ActivityID = "FF0C5A9C-86B4-4378-BA8F-C1CE165BERE",
                  Description = "Patent Medicine",
                  Endorsement = "Public Health: Food",
                  CategoryCode = "9333",
                  UnitOne = "Units",
                  UnitTwo = "NA",
                  App_Type = "B",
                  IsSecondaryLicenseCategory = true,
                  IsSubCategory = false,
                  Status = true
              });
             AddMasterPrimaryCategoryEntity(new MasterPrimaryCategory()
              {
                  PrimaryID = "DB781A05-9B0F-4AA3-A7D6-C22F3E774C03",
                  ActivityID = "1ED33685-B54E-4D7A-BBCE-1C93BD762CB1",
                  Description = "General Business Licenses",
                  Endorsement = "General Business",
                  CategoryCode = "4003",
                  UnitOne = "NA",
                  UnitTwo = "NA",
                  App_Type = "B",
                  IsSecondaryLicenseCategory = true,
                  IsSubCategory = true,
                  Status = true
              });
           				
              AddMasterPrimaryCategoryEntity(new MasterPrimaryCategory()
              {
                  PrimaryID = "E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                  ActivityID = "FF0C5A9C-86B4-4378-BA8F-C1CE165B814E",
                  Description = "Restaurant",
                  Endorsement = "Public Health: Food",
                  CategoryCode = "9313",
                  UnitOne = "Rooms",
                  UnitTwo = "Kitchens",
                  App_Type = "B",
                  IsSecondaryLicenseCategory = true,
                  IsSubCategory = false,
                  Status = true
              });


              AddMasterPrimaryCategoryEntity(new MasterPrimaryCategory()
              {
                  PrimaryID = "26E9FC09-2763-4751-B880-EC4E805DF281",
                  ActivityID = "1ED33685-B54E-4D7A-BBCE-1C93BD762CB1",
                  Description = "Solicitor",
                  Endorsement = "General Sales",
                  CategoryCode = "4112",
                  UnitOne = "Units",
                  UnitTwo = "NA",
                  App_Type = "I",
                  IsSecondaryLicenseCategory = true,
                  IsSubCategory = false,
                  Status = true
              });


              AddMasterPrimaryCategoryEntity(new MasterPrimaryCategory()
              {
                  PrimaryID = "1B7A6DC1-5B80-493F-B83D-C3A812E458DB",
                  ActivityID = "FF0C5A9C-86B4-4378-BA8F-C1CE165B814E",
                  Description = "APARTMENT",
                  Endorsement = "Public Health: Food",
                  CategoryCode = "9306",
                  UnitOne = "Flats",
                  UnitTwo = "NA",
                  App_Type = "I",
                  IsSecondaryLicenseCategory = true,
                  IsSubCategory = false,
                  Status = true
              });
        }

    }
}