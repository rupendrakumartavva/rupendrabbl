using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class Portal_Content_ErrorsRepositoryData
    {
        private readonly List<Portal_Content_Errors> _entities;
        public bool IsInitialized;

        public void AddPortalContentErrorEntity(Portal_Content_Errors obj)
        {
            _entities.Add(obj);
        }

        public List<Portal_Content_Errors> PortalContentEntitiesList
        {
            get { return _entities; }
        }


        public Portal_Content_ErrorsRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<Portal_Content_Errors>();

            AddPortalContentErrorEntity(new Portal_Content_Errors()
            {
                MessageId = "0e04bb3e-51e6-441d-87a9-0d6bd29261fd",
                MessageType = "Success",
                ShortName = "Inserted",
                ErrrorMessage = "Activity Inserted SuccssFully",
                IsActive = true,
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now
            });


        }
    }
}
