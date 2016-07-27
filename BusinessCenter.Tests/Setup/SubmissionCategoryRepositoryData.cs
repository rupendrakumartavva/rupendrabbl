using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
  public class SubmissionCategoryRepositoryData
    {
       private readonly List<SubmissionCategory> _entities;
        public bool IsInitialized;

        public void AddSubmissionCategoryEntity(SubmissionCategory obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionCategory> SubmissionCategoryEntitiesList
        {
            get { return _entities; }
        }


        public SubmissionCategoryRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionCategory>();

            AddSubmissionCategoryEntity(new SubmissionCategory()
            {
                SubmissionCategoryID = 1,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                CategoryTypeID = "6C548EBE-59ED-40B8-BEBE-EDF79149295D",
                EndorsementFee = Convert.ToDecimal(25.0000),
                LicenseCategoryFee = Convert.ToDecimal(380.0000),
                CategoryType = "PRIMARY",
                ItemQty = "20,2",
                CreatedDate=null,
                UpdateDate=null,
                Endorsement = "Housing: Transient"

            });

            AddSubmissionCategoryEntity(new SubmissionCategory()
            {
                SubmissionCategoryID = 2,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                CategoryTypeID = "42D8D51F-6B22-4E7B-BC7F-CE0EA2E2814B",
                EndorsementFee = Convert.ToDecimal(25.0000),
                LicenseCategoryFee = Convert.ToDecimal(400.0000),
                CategoryType = "SECONDARYCATEGORY",
                ItemQty = "NA",
                CreatedDate = null,
                UpdateDate = null,
                Endorsement = "Public Health: Food"

            });

            AddSubmissionCategoryEntity(new SubmissionCategory()
            {
                SubmissionCategoryID = 3,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                CategoryTypeID = "891F943E-D4D9-4660-872C-26366BB1C197",
                EndorsementFee = Convert.ToDecimal(0.0000),
                LicenseCategoryFee = Convert.ToDecimal(673.0000),
                CategoryType = "SECONDARYCATEGORY",
                ItemQty = "100",
                CreatedDate = null,
                UpdateDate = null,
                Endorsement = "Public Health: Food"


            });

            AddSubmissionCategoryEntity(new SubmissionCategory()
            {
                SubmissionCategoryID = 4,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                CategoryTypeID = "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",
                EndorsementFee = Convert.ToDecimal(0.0000),
                LicenseCategoryFee = Convert.ToDecimal(0.0000),
                CategoryType = "PRIMARY",
                ItemQty = "NA",
                CreatedDate = null,
                UpdateDate = null,
                Endorsement = "General Business"

            });
            AddSubmissionCategoryEntity(new SubmissionCategory()
            {
                SubmissionCategoryID = 4,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                CategoryTypeID = "891F943E-D4D9-4660-872C-26366BB1C197",
                EndorsementFee = Convert.ToDecimal(0.0000),
                LicenseCategoryFee = Convert.ToDecimal(0.0000),
                CategoryType = "SECONDARYCATEGORY",
                ItemQty = "NA",
                CreatedDate = null,
                UpdateDate = null,
                Endorsement = "General Business"

            });

            AddSubmissionCategoryEntity(new SubmissionCategory()
            {
                SubmissionCategoryID = 5,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                CategoryTypeID = "16FD71A3-7DD7-4B65-A006-777691BCB7FC",
                EndorsementFee = Convert.ToDecimal(25.0000),
                LicenseCategoryFee = Convert.ToDecimal(578.0000),
                CategoryType = "PRIMARY",
                ItemQty = "NA",
                CreatedDate = null,
                UpdateDate = null,
                Endorsement = "Inspected Sales and Services"

            });

            AddSubmissionCategoryEntity(new SubmissionCategory()
            {
                SubmissionCategoryID = 6,
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                CategoryTypeID = "26E9FC09-2763-4751-B880-EC4E805DF281",
                EndorsementFee = Convert.ToDecimal(25.0000),
                LicenseCategoryFee = Convert.ToDecimal(411.0000),
                CategoryType = "PRIMARY",
                ItemQty = "NA",
                CreatedDate = null,
                UpdateDate = null,
                Endorsement = "General Sales"

            });

            AddSubmissionCategoryEntity(new SubmissionCategory()
            {
                SubmissionCategoryID = 7,
                MasterId = "38de17fc-4254-400f-a889-09a665c9adda",
                CategoryTypeID = "0495A756-2E3C-4444-8B6F-993445502401",
                EndorsementFee = Convert.ToDecimal(25.0000),
                LicenseCategoryFee = Convert.ToDecimal(511.0000),
                CategoryType = "SUBCATEGORY",
                ItemQty = "NA",
                CreatedDate = null,
                UpdateDate = null,
                Endorsement = "General Sales"

            });
        }
    }
}
