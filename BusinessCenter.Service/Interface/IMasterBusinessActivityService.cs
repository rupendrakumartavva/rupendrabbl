using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
    public interface IMasterBusinessActivityService
    {
        IEnumerable<MasterBusinessActivity> GetAllBusinessActivities();
        IEnumerable<MasterBusinessActivity> FindBusinessActivitiesById(BusinessActivityModel businessActivityModel);
        int InsertUpdateBusinessActivity(BusinessActivityEntity businessActivityModel);
        bool DeleteBusinessActivity(BusinessActivityEntity businessActivityModel);
        IEnumerable<MasterBusinessActivity> GetBusinessActivity();
        IEnumerable<MasterBusinessActivity> FindByIDBasedonActivityId(BusinessActivityModel businessActivityModel);
    }
}
