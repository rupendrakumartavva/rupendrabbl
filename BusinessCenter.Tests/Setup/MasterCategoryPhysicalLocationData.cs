using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class MasterCategoryPhysicalLocationData
    {
        private readonly List<MasterCategoryPhysicalLocation> _entities;
        public bool IsInitialized;

        public void AddMasterCategoryPhysicalLocationEntity(MasterCategoryPhysicalLocation obj)
        {
            _entities.Add(obj);
        }

        public List<MasterCategoryPhysicalLocation> MasterCategoryPhysicalLocationList
        {
            get { return _entities; }
        }
        public MasterCategoryPhysicalLocationData()
        {
            IsInitialized = true;
            _entities = new List<MasterCategoryPhysicalLocation>();
            AddMasterCategoryPhysicalLocationEntity(new MasterCategoryPhysicalLocation()
           {
               RuleId = 1,
               PrimaryCategory_Id = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
               BusinessMustBeInDC = "Yes",
               COFORequired = "No",
               HOP_EHOPAllowed = "No",
               ExemptFromAllFees = "No",
               LicenseType = "B",
               Status = true
           });
            AddMasterCategoryPhysicalLocationEntity(new MasterCategoryPhysicalLocation()
            {
                RuleId = 2,
                PrimaryCategory_Id = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",
                BusinessMustBeInDC = "No",
                COFORequired = "No",
                HOP_EHOPAllowed = "Yes",
                ExemptFromAllFees = "Yes",
                LicenseType = "B",
                Status = true
            });
            AddMasterCategoryPhysicalLocationEntity(new MasterCategoryPhysicalLocation()
            {
                RuleId = 3,
                PrimaryCategory_Id = "16FD71A3-7DD7-4B65-A006-777691BCB7FC",
                BusinessMustBeInDC = "No",
                COFORequired = "No",
                HOP_EHOPAllowed = "No",
                ExemptFromAllFees = "No",
                LicenseType = "i",
                Status = true
            });
            AddMasterCategoryPhysicalLocationEntity(new MasterCategoryPhysicalLocation()
            {
                RuleId = 4,
                PrimaryCategory_Id = "BBD6F69D-A82F-4A46-AC21-0E08E29FD600",
                BusinessMustBeInDC = "No",
                COFORequired = "No",
                HOP_EHOPAllowed = "Yes",
                ExemptFromAllFees = "No",
                LicenseType = "B",
                Status = true
            });
            AddMasterCategoryPhysicalLocationEntity(new MasterCategoryPhysicalLocation()
            {
                RuleId = 5,
                PrimaryCategory_Id = "796B2CFD-EBDC-4480-B45C-502A816860FB",
                BusinessMustBeInDC = "No",
                COFORequired = "Yes",
                HOP_EHOPAllowed = "No",
                ExemptFromAllFees = "No",
                LicenseType = "B",
                Status = true
            });
            AddMasterCategoryPhysicalLocationEntity(new MasterCategoryPhysicalLocation()
            {
                RuleId = 6,
                PrimaryCategory_Id = "640C7E4B-8F90-4E59-9BA5-D1EA71E7B6FB",
                BusinessMustBeInDC = "No",
                COFORequired = "Yes",
                HOP_EHOPAllowed = "No",
                ExemptFromAllFees = "No",
                LicenseType = "B",
                Status = true
            });
            AddMasterCategoryPhysicalLocationEntity(new MasterCategoryPhysicalLocation()
            {
                RuleId = 7,
                PrimaryCategory_Id = "DB781A05-9B0F-4AA3-A7D6-C22F3E774C03",
                BusinessMustBeInDC = "No",
                COFORequired = "No",
                HOP_EHOPAllowed = "Yes",
                ExemptFromAllFees = "No",
                LicenseType = "B",
                Status = true
            });
            AddMasterCategoryPhysicalLocationEntity(new MasterCategoryPhysicalLocation()
            {
                RuleId = 8,
                PrimaryCategory_Id = "E037E31E-9DA2-47F0-A2AD-7FFF385EB1B3",
                BusinessMustBeInDC = "Yes",
                COFORequired = "Yes",
                HOP_EHOPAllowed = "No",
                ExemptFromAllFees = "No",
                LicenseType = "B",
                Status = true
            });

            AddMasterCategoryPhysicalLocationEntity(new MasterCategoryPhysicalLocation()
            {
                RuleId = 9,
                PrimaryCategory_Id = "26E9FC09-2763-4751-B880-EC4E805DF281",
                BusinessMustBeInDC = "No",
                COFORequired = "No",
                HOP_EHOPAllowed = "Yes",
                ExemptFromAllFees = "No",
                LicenseType = "I",
                Status = true
            });
        }
    }
}
