using BusinessCenter.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Models
{
    public class SearchDataViewModel
    {
        public int ID { get; set; }
        public string RecordCount { get; set; }
        public int NoofRecords { get; set; }
        public int ABRAID { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
        public string ABRACount { get; set; }
        public string BBLCount { get; set; }
        public string CBECount { get; set; }
        public string CORPCount { get; set; }
        public string OPLACount { get; set; }
        public List<SearchDataViewModel> TotalSearchList { get; set; }
        public List<CommonData> FinalData { get; set; }

        public string ExcededBbl { get; set; }
        public string ExcededOpla { get; set; }
        public string ExcededCorp { get; set; }
        public string ExcededAbra { get; set; }
        public string ExcededCbp { get; set; }
        public int ExcededCount { get; set; }
        public string ExcededRegulatoryEntities { get; set; }
    }

    public class SearchServiceInputs
    {
        public string Userid { get; set; }
        public string SearchString { get; set; }
        public string SearchType { get; set; }
        public string CompanyName { get; set; }
        public string LicenseName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool IsChanged { get; set; }
        public string DisplayType { get; set; }
        public string KeyType { get; set; }
    }

    public class InputCheckedValues
    {
        public string Bbl { get; set; }
        public string Corp { get; set; }
        public string Opla { get; set; }
        public string Cbe { get; set; }
        public string Abra { get; set; }
    }

    public class SearchResultData
    {
        public  IQueryable<CommonData> FilledData { get; set; }
        public  IQueryable<UserService> MultiUserData { get; set; }
        public  IQueryable<CommonData> SavedFilledData { get; set; }
        public  ICollection<DCBC_ENTITY_BBL> BblData { get; set; }
        public ICollection<DCBC_ENTITY_ABRA> AbraData { get; set; }
        public ICollection<DCBC_ENTITY_CBE> CbeData { get; set; }
        public ICollection<DCBC_ENTITY_OPLA> OplaData { get; set; }
        public ICollection<DCBC_ENTITY_CORP> CorpData { get; set; }
    }

    public class CommonData
    {
        public int id { get; set; }
        public bool WishList { get; set; }
        public int EntityID { get; set; }
        public string Source { get; set; }
        public string CompanyName { get; set; }
        public string LicenseNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LeftNameTop { get; set; }
        public string LeftNameMiddle { get; set; }
        public string LeftNameBottom { get; set; }
        public string MiddleNameTop { get; set; }
        public string MiddleNameMiddle { get; set; }
        public string MiddleNameBottom { get; set; }
        public string RightNameTop { get; set; }
        public string RightNameMiddle1 { get; set; }
        public string RightNameMiddle2 { get; set; }
        public string RightNameBottom { get; set; }
        public string Expantion1 { get; set; }
        public string Expantion2 { get; set; }
        public string Expantion3 { get; set; }
        public string Expantion4 { get; set; }
        public string Expantion5 { get; set; }
        public string Expantion6 { get; set; }

        public string LeftNameResultTop { get; set; }
        public string LeftNameResultMiddle { get; set; }
        public string LeftNameResultBottom { get; set; }
        public string MiddleNameResultTop { get; set; }
        public string MiddleNameResultMiddle { get; set; }
        public string MiddleNameResultBottom { get; set; }
        public string RightNameResultTop { get; set; }
        public string RightNameResultMiddle1 { get; set; }
        public string RightNameResultMiddle2 { get; set; }
        public string RightNameResultBottom { get; set; }
        public string ExpantionResult1 { get; set; }
        public string ExpantionResult2 { get; set; }
        public string ExpantionResult3 { get; set; }
        public string ExpantionResult4 { get; set; }
        public string ExpantionResult5 { get; set; }
        public string ExpantionResult6 { get; set; }
        public string SourceFullName { get; set; }
        public string LastUpdateDateName { get; set; }
        public string LastUpdateDate { get; set; }

        public string LeftNameMiddleLabel1 { get; set; }
        public string LeftNameMiddle1Text { get; set; }
    }

    public class SearchData
    {
        public virtual ICollection<SearchDataViewModel> SearchCount { get; set; }
        public virtual ICollection<CommonData> SearchFinalData { get; set; }
    }
}