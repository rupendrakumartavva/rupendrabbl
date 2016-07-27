using BusinessCenter.Data;
using BusinessCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class UserServicesRepositoryData
    {
        private readonly List<UserService> _entities;
        public bool IsInitialized;
        private readonly List<CommonData> _commonData;


        public void AddUserServiceEntity(UserService obj)
        {
            _entities.Add(obj);
        }

        public List<UserService> UserServiceEntitiesList
        {
            get { return _entities; }
        }


        public void AddCommonData(CommonData obj)
        {
            _commonData.Add(obj);
        }

        public List<CommonData> CommonDataEntitiesList
        {
            get { return _commonData; }
        }

        public UserServicesRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<UserService>();
            _commonData = new List<CommonData>();

            AddUserServiceEntity(new UserService()
            {
                UserListId = 100,
                DATA_SOURCE = "OPLA",
                DCBC_ENTITY_ID = 50004151,
                CreatedDate = DateTime.Now,
                Status = "Active",
                CreatedBy = 22,
                UserId = "E5641464-F7A4-499C-9AF2-E450C8C26796"
            });
            AddUserServiceEntity(new UserService()
            {
                UserListId = 101,
                DATA_SOURCE = "BBL",
                DCBC_ENTITY_ID = 10045105,
                CreatedDate = DateTime.Now,
                Status = "Active",
                CreatedBy = 14,
                UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4"
            });
            AddUserServiceEntity(new UserService()
            {
                UserListId = 102,
                DATA_SOURCE = "CORP",
                DCBC_ENTITY_ID = 40072773,
                CreatedDate = DateTime.Now,
                Status = "Active",
                CreatedBy = 14,
                UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4"
            });
            AddUserServiceEntity(new UserService()
            {
                UserListId = 103,
                DATA_SOURCE = "CORP",
                DCBC_ENTITY_ID = 40102957,
                CreatedDate = DateTime.Now,
                Status = "Active",
                CreatedBy = 14,
                UserId = "A583A22D-FD47-4816-8A2E-5B17702323D4"
            });

            AddUserServiceEntity(new UserService()
            {
                UserListId = 104,
                DATA_SOURCE = "ABRA",
                DCBC_ENTITY_ID = 20007358,
                CreatedDate = DateTime.Now,
                Status = "Active",
                CreatedBy = 5,
               // UserId = "2ED4C269-F244-486B-8323-A2BA96408FE3"
                UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613"
            });

            AddUserServiceEntity(new UserService()
            {
                UserListId = 105,
                DATA_SOURCE = "CBE",
                DCBC_ENTITY_ID = 30000001,
                CreatedDate = DateTime.Now,
                Status = "Active",
                CreatedBy = 5,
                // UserId = "2ED4C269-F244-486B-8323-A2BA96408FE3"
                UserId = "2AC53E53-87D0-468F-9628-4AEBA1120613"
            });

      //      AddCommonData(new CommonData()
      //     {
      //           id= ,
      //WishList =,
      // EntityID =,
      // Source =,
      //  CompanyName=,
      // LicenseNumber=,
      // FirstName=,
      //  LastName =,
      //  LeftNameTop=,
      // LeftNameMiddle=,
      // LeftNameBottom=,
      //  MiddleNameTop=,
      // MiddleNameMiddle=,
      //  MiddleNameBottom=,
      //  RightNameTop=,
      //  RightNameMiddle1=,
      //   RightNameMiddle2=,
      //   RightNameBottom=,
      // Expantion1=,
      //  Expantion2=,
      // Expantion3=,
      // Expantion4=,
      //  Expantion5=,
      // Expantion6=,

      //  LeftNameResultTop=,
      //  LeftNameResultMiddle=,
      //  LeftNameResultBottom=,
      //  MiddleNameResultTop=,
      //  MiddleNameResultMiddle=,
      //  MiddleNameResultBottom=,
      //  RightNameResultTop=,
      //  RightNameResultMiddle1=,
      // RightNameResultMiddle2=,
      //  RightNameResultBottom=,
      // ExpantionResult1=,
      //  ExpantionResult2=,
      //  ExpantionResult3=,
      //   ExpantionResult4=,
      // ExpantionResult5=,
      //  ExpantionResult6=,
      //  SourceFullName=,
      // LastUpdateDateName=,
      // LastUpdateDate=,

      //LeftNameMiddleLabel1=,
      // LeftNameMiddle1Text=,
      //     });

           
        }
    }
}
