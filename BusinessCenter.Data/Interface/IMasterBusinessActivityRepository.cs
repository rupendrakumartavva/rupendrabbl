using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IMasterBusinessActivityRepository
    {
        IEnumerable<MasterBusinessActivity> AllBusinessActivities();
        IEnumerable<MasterBusinessActivity> FindByID(BusinessActivityModel businessActivityModel);
        IEnumerable<MasterBusinessActivity> FindByActivityName(RenewModel renewModel);
        int InsertUpdateBusinessActivity(BusinessActivityEntity businessActivityModel);
        bool DeleteBusinessActivity(BusinessActivityEntity businessActivityModel);
        IEnumerable<MasterBusinessActivity> GetBusinessActivity();
        IEnumerable<MasterBusinessActivity> FindByActivityId(string activityId);
        IEnumerable<MasterBusinessActivity> FindByIDBasedonActivityId(BusinessActivityModel businessActivityModel);
    }
}
