using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
   public interface ILookup_ExistingCategoriesRepository
    {
   
       IEnumerable<Lookup_ExistingCategories> FindBy(string categoryName);
       bool InsertUpdateCategoryLookUp(CategoryLookup categoryLookup);
       IEnumerable<Lookup_ExistingCategories> NewCategoryFindBy(string categoryName);
    }
}
