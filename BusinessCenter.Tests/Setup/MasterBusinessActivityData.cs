using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{

    public class MasterBusinessActivityData
    {
        private readonly List<MasterBusinessActivity> _entities;
        public bool IsInitialized;

        public void AddMasterAcivityEntity(MasterBusinessActivity obj)
        {
            _entities.Add(obj);
        }

        public List<MasterBusinessActivity>MasterAcivityEntitiesList
        {
            get { return _entities; }
        }


        public MasterBusinessActivityData()
        {
            IsInitialized = true;
            _entities = new List<MasterBusinessActivity>();

            AddMasterAcivityEntity(new MasterBusinessActivity()
            {
                ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
                ActivityName = "Real Estate & Rentals",
                APP_Type = "1"
            });
            AddMasterAcivityEntity(new MasterBusinessActivity()
            {
                ActivityID = "BE29F663-A3FA-4B64-8697-C9C4FF91B69F",
                ActivityName = "Charity",
                APP_Type = "1"
            });
            AddMasterAcivityEntity(new MasterBusinessActivity()
            {
                ActivityID = "668F4B68-8437-431F-9052-60809E556028",
                ActivityName = "Used Goods Dealing & Sales",
                APP_Type = "1"
            });
            AddMasterAcivityEntity(new MasterBusinessActivity()
            {
                ActivityID = "C49EACE2-2180-430E-BA2F-8B891D659421",
                ActivityName = "Home Improvement and Security",
                APP_Type = "1"
            });

            AddMasterAcivityEntity(new MasterBusinessActivity()
            {
                ActivityID = "1ED33685-B54E-4D7A-BBCE-1C93BD762CB1",
                ActivityName = "Retail Sales, Consulting, and Other Services",
                APP_Type = "1"
            });

            //AddMasterAcivityEntity(new MasterBusinessActivity()
            //{
            //    ActivityID = "1ED33685-B54E-4D7A-BBCE-1C93BD762CB1",
            //    ActivityName = "Retail Sales, Consulting, and Other Services",
            //    APP_Type = "1"
            //});

        }
    }
}