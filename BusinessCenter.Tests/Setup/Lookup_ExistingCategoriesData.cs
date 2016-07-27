using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
   public class Lookup_ExistingCategoriesData
    {
        private readonly List<Lookup_ExistingCategories> _entities;       

        public void AddExistingCategoriesEntity(Lookup_ExistingCategories obj)
        {
            _entities.Add(obj);
        }

        public List<Lookup_ExistingCategories> ExistingCategoriesList
        {
            get { return _entities; }
        }
        public Lookup_ExistingCategoriesData()
        {

            _entities = new List<Lookup_ExistingCategories>();


            AddExistingCategoriesEntity(new Lookup_ExistingCategories()
            {
                LookUpId=1,
                NewCategoryName = "Hotel",
                ExistingCategory = "Hotel"
            });
            AddExistingCategoriesEntity(new Lookup_ExistingCategories()
            {
                LookUpId=2,
                NewCategoryName = "Charitable Exempt",
                ExistingCategory = "Charitable Exempt"
            });
            AddExistingCategoriesEntity(new Lookup_ExistingCategories()
            {
                LookUpId=3,
                NewCategoryName = "General Business",
                ExistingCategory = "General Business"
            });
            AddExistingCategoriesEntity(new Lookup_ExistingCategories()
            {
                LookUpId = 4,
                NewCategoryName = "Cigarette Retail",
                ExistingCategory = "Cigarette Retail"
            });
            AddExistingCategoriesEntity(new Lookup_ExistingCategories()
            {
                LookUpId = 4,
                NewCategoryName = "Restaurant",
                ExistingCategory = "Restaurant"
            });
        }
    }
}
