using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class Lookup_ExistingCategoriesRepository : GenericRepository<Lookup_ExistingCategories>,ILookup_ExistingCategoriesRepository
    {
        public Lookup_ExistingCategoriesRepository(IUnitOfWork context)
            : base(context)
         {

         }
        /// <summary>
        ///  This method is used to retrive Lookup_ExistingCategories based on NewCategoryName
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>Retrun Lookup_ExistingCategories</returns>
        public IEnumerable<Lookup_ExistingCategories> NewCategoryFindBy(string categoryName)
        {
            var category = FindBy(x => x.NewCategoryName == categoryName).ToList();
            return category;
        }
        /// <summary>
        /// This method is used to retrive Lookup_ExistingCategories based on ExistingCategory
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>Retrun Lookup_ExistingCategories</returns>
        public IEnumerable<Lookup_ExistingCategories> FindBy(string categoryName)
        {
            var category=FindBy(x=>x.ExistingCategory==categoryName).ToList();
            return category;
        }
        /// <summary>
        /// This method is used to Insert/Update Lookup_ExistingCategories based on user inputs
        /// </summary>
        /// <param name="categoryLookup"></param>
        /// <returns>Return bool status</returns>
        public bool InsertUpdateCategoryLookUp(CategoryLookup categoryLookup)
        {
            bool status=false;
            try { 
            
            var CategoryExist = FindBy(x => x.LookUpId == categoryLookup.LookUpId).ToList();
            if(CategoryExist.Count()==0)
            {
                Lookup_ExistingCategories Categories = new Lookup_ExistingCategories();
                Categories.ExistingCategory = (categoryLookup.ExistingCategory ?? "").Trim();
                Categories.NewCategoryName = (categoryLookup.NewCategoryName ?? "").Trim();
                Add(Categories);
                Save();
                status = true;
            }
            else
            {
                var updateCategory = CategoryExist.SingleOrDefault();
                updateCategory.ExistingCategory = (categoryLookup.ExistingCategory ?? "").Trim();
                updateCategory.NewCategoryName = (categoryLookup.NewCategoryName ?? "").Trim();
                Save();
                status = true;
            }
                }
            catch(Exception )
            { status = false; }
            return status;
        }
    }
}
