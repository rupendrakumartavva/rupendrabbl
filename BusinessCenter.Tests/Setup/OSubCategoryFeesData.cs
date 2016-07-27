using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class OSubCategoryFeesData
    {
        private readonly List<OSub_Category_Fees> _entities;
        public bool IsInitialized;

        public void AddOSubCategoryFeesEntity(OSub_Category_Fees obj)
        {
            _entities.Add(obj);
        }

        public List<OSub_Category_Fees> OSubCategoryFeesEntitiesList
        {
            get { return _entities; }
        }

        public OSubCategoryFeesData()
        {
            IsInitialized = true;
            _entities = new List<OSub_Category_Fees>();

            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                 OSub_Category = "1",
                OSub_Description = "Restaurant",
                Fee_Code = "T",
                Start = 1,
                End = 10,
                License_Fee = (decimal?)450.0000,
                Tier = null,
                App_Type = "B",
                Status = true
             
            });
            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                OSub_Category = "2",
                OSub_Description = "Restaurant",
                Fee_Code = "T",
                Start = 11,
                End = 50,
                License_Fee = (decimal?)562.0000,
                Tier = null,
                App_Type = "B",
                Status = true
            });
            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                OSub_Category = "3",
                OSub_Description = "Restaurant",
                Fee_Code = "T",
                Start = 51,
                End = 100,
                License_Fee = (decimal?)673.0000,
                Tier = null,
                App_Type = "B",
                Status = true
            });

            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                OSub_Category = "4",
                OSub_Description = "Restaurant",
                Fee_Code = "TA",
                Start = 101,
                End = 99999,
                License_Fee = (decimal?)785.0000,
                Tier = 1,
                App_Type = "B",
                Status = true
            });

            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                OSub_Category = "5",
                OSub_Description = "General Business Licenses",
                Fee_Code = "S",
                Start = 0,
                End = 99999,
                License_Fee = (decimal?)400.0000,
                Tier = null,
                App_Type = "B",
                Status = true
            });

            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                OSub_Category = "6",
                OSub_Description = "Hotel",
                Fee_Code = "C",
                Start = 0,
                End = 99999,
                License_Fee = (decimal?)21.0000,
                Tier = 1,
                App_Type = "B",
                Status = true
            });
            //AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            //{
            //    OSub_Category = "9",
            //    OSub_Description = "Hotel",
            //    Fee_Code = "T",
            //    Start = 51,
            //    End = 99999,
            //    License_Fee = (decimal?)45.00,
            //    Tier = 10,
            //    App_Type = "B",
            //    Status = true
            //});
            //AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            //{
            //    OSub_Category = "10",
            //    OSub_Description = "Hotel",
            //    Fee_Code = "TA",
            //    Start = 52,
            //    End = 99999,
            //    License_Fee = (decimal?)45.00,
            //    Tier = 10,
            //    App_Type = "B",
            //    Status = true
            //});
            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                OSub_Category = "7",
                OSub_Description = "Food Vending Machine",
                Fee_Code = "T",
                Start = 1,
                End = 100,
                License_Fee = (decimal?)21.0000,
                Tier = null,
                App_Type = "B",
                Status = true
            });
            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                OSub_Category = "10",
                OSub_Description = "Food Vending Machine",
                Fee_Code = "TA",
                Start = 101,
                End = 99999,
                License_Fee = (decimal?)21.0000,
                Tier = 2,
                App_Type = "B",
                Status = true
            });
            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                OSub_Category = "8",
                OSub_Description = "Solicitor",
                Fee_Code = "C",
                Start = 0,
                End = 99999,
                License_Fee = (decimal?)411.0000,
                Tier =2,
                App_Type = "I",
                Status = true
            });
            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                OSub_Category = "9",
                OSub_Description = "APARTMENT",
                Fee_Code = "C",
                Start = 0,
                End = 99999,
                License_Fee = (decimal?)411.0000,
                Tier = 1,
                App_Type = "I",
                Status = true
            });

            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                OSub_Category = "11",
                OSub_Description = "Restaurant",
                Fee_Code = "H",
                Start = 1,
                End = 10,
                License_Fee = (decimal?)450.0000,
                Tier = null,
                App_Type = "B",
                Status = true

            });
            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                OSub_Category = "12",
                OSub_Description = "Restaurant",
                Fee_Code = "HT",
                Start = 11,
                End = 99999,
                License_Fee = (decimal?)562.0000,
                Tier = 1,
                App_Type = "B",
                Status = true
            });

            AddOSubCategoryFeesEntity(new OSub_Category_Fees()
            {
                OSub_Category = "13",
                OSub_Description = "ONE FAMILY RENTAL",
                Fee_Code = "S",
                Start = 0,
                End = 99999,
                License_Fee = (decimal?)562.0000,
                Tier = null,
                App_Type = "B",
                Status = true
            });
        }
         
    }
}