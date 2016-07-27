using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class MasterSubCategoryRepositoryData
    {
        private readonly List<MasterSubCategory> _entities;
        public bool IsInitialized;

        public void AddMasterSubCategoryEntity(MasterSubCategory obj)
        {
            _entities.Add(obj);
        }

        public IEnumerable<MasterSubCategory> MasterSubCategoryList
        {
            get { return _entities; }
        }

        public MasterSubCategoryRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<MasterSubCategory>();

            AddMasterSubCategoryEntity(new MasterSubCategory()
            {
                SubCatID = "0495A756-2E3C-4444-8B6F-993445502401",
                SubCategoryName = "Shoe Cleaning/Repair",
                CustomCategoryName = "General Business Licenses",
                Status = true
            });
            AddMasterSubCategoryEntity(new MasterSubCategory()
            {
                SubCatID = "0AB4C44F-E720-4D96-A1FD-1BFA98752493",
                SubCategoryName = "Dog Day Care",
                CustomCategoryName = "General Business Licenses",
                Status = true
            });

            AddMasterSubCategoryEntity(new MasterSubCategory()
            {
                SubCatID = "14DDF60B-85C1-4AE3-AC6D-386F715E3361",
                SubCategoryName = "Collection Agencies",
                CustomCategoryName = "General Business Licenses",
                Status = true
            });

            AddMasterSubCategoryEntity(new MasterSubCategory()
            {
                SubCatID = "2DE6F1DD-CDC5-4844-8D7B-4085A1D1E68E",
                SubCategoryName = "Dog Walkers",
                CustomCategoryName = "General Business Licenses",
                Status = true
            });
            AddMasterSubCategoryEntity(new MasterSubCategory()
            {
                SubCatID = "ee4bb841-89c1-495c-941f-283e3421822e",
                SubCategoryName = "Hotel SubCategory",
                CustomCategoryName = "Hotel",
                Status = true
            });

        }
    }
}