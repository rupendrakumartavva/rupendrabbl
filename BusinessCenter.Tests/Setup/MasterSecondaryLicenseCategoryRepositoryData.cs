using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class MasterSecondaryLicenseCategoryRepositoryData
    {
        private readonly List<MasterSecondaryLicenseCategory> _entities;
        public bool IsInitialized;

        public void AddMasterSeconderyLicenseCategoryEntity(MasterSecondaryLicenseCategory obj)
        {
            _entities.Add(obj);
        }

        public IEnumerable<MasterSecondaryLicenseCategory> MasterSeconderyLicenseCategoryList
        {
            get { return _entities; }
        }

        public MasterSecondaryLicenseCategoryRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<MasterSecondaryLicenseCategory>();

            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "1DE53FC4-F444-44EA-99A4-5DA3F107D5DD",
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                SecondaryLicenseCategory = "Food Vending Machine",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = false,
                Status = true
            });
            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "42D8D51F-6B22-4E7B-BC7F-CE0EA2E2814B",
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                SecondaryLicenseCategory = "APARTMENT",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = false,
                Status = true
            });
            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "4A51BC6C-A946-4A36-9B7E-BCB4722118E3",
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                SecondaryLicenseCategory = "Swimming Pool",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = false,
                Status = true
            });
            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "78837896-8FA9-4240-8E22-2F5CC02FA24E",
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                SecondaryLicenseCategory = "Patent Medicine",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = false,
                Status = true
            });
            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "891F943E-D4D9-4660-872C-26366BB1C197",
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                SecondaryLicenseCategory = "Restaurant",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = false,
                Status = true
            });
            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "F1E7E515-A8A8-4EDC-B2E8-8BBA8B6D30AF",
                PrimaryID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                SecondaryLicenseCategory = "Public Hall",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = false,
                Status = true
            });

            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "1B69B449-ECAB-41AE-89A9-E2F76329A52B",
                PrimaryID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",
                SecondaryLicenseCategory = "General Business Licenses",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = true,
                Status = true
            });

            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "E021FB1B-9C90-456F-AEC3-F081B51A0270",
                PrimaryID = "16FD71A3-7DD7-4B65-A006-777691BCB7FC",
                SecondaryLicenseCategory = "NA",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = false,
                Status = true
            });
            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "E80BB15D-71D0-4BC2-8E6F-B31F9B311027",
                PrimaryID = "BBD6F69D-A82F-4A46-AC21-0E08E29FD600",
                SecondaryLicenseCategory = "NA",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = false,
                Status = true
            });
            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "4402E4B3-BA07-4D1E-8660-76A64D540702",
                PrimaryID = "796B2CFD-EBDC-4480-B45C-502A816860FB",
                SecondaryLicenseCategory = "NA",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = false,
                Status = true
            });
            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "640C7E4B-8F90-4E59-9BA5-D1EA71E7B6AA",
                PrimaryID = "640C7E4B-8F90-4E59-9BA5-D1EA71E7B6FB",
                SecondaryLicenseCategory = "Food Vending Machine",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = false,
                Status = true
            });
            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "4402E4B3-BA07-4D1E-8660-76A64D559772",
                PrimaryID = "796B2CFD-EBDC-4480-B45C-502A81686999",
                SecondaryLicenseCategory = "NA",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = false,
                Status = true
            });

            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "4402E4B3-BA07-4D1E-8660-76A64D559779",
                PrimaryID = "640C7E4B-8F90-4E59-9BA5-D1EA71E7BSEA",
                SecondaryLicenseCategory = "Patent Medicine",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = false,
                Status = true
            });

            AddMasterSeconderyLicenseCategoryEntity(new MasterSecondaryLicenseCategory()
            {
                SecondaryID = "1B69B449-ECAB-41AE-89A9-E2F76329A866",
                PrimaryID = "65868F7E-6053-4645-BA36-95635ACFEB4B",
                SecondaryLicenseCategory = "Patent Medicine",
                Endorsement = "",
                UnitOne = "",
                UnitTwo = "",
                IsSubCategory = true,
                Status = true
            });
        }
    }
}