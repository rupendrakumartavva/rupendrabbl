using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessCenter.Data.Implementation
{
    public class MasterBusinessActivityRepository : GenericRepository<MasterBusinessActivity>, IMasterBusinessActivityRepository
    {
        protected IMasterPrimaryCategoryRepository _primaryrepo;
        public MasterBusinessActivityRepository(IUnitOfWork context, IMasterPrimaryCategoryRepository primaryrepo)
            : base(context)
        {
            _primaryrepo = primaryrepo;
        }

        // TODO: May be we need to Change this .
        /// <summary>
        /// This method is used to display all the Descending  Business Activities for Anuglar and admin as well based on CreatedDate .
        /// </summary>
        /// <returns>Business Activity Data</returns>
        public IEnumerable<MasterBusinessActivity> AllBusinessActivities()
        {
               var bussinessAcvitity = GetAll().AsQueryable().OrderByDescending(x => x.CreateDate);
                return bussinessAcvitity;
         
        }

        /// <summary>
        /// This method is used to display all the  Business Activities in Alphabetical order for Anuglar and admin as well based on App_type=1 .
        /// </summary>
        /// <returns>Business Activity Data</returns>
        public IEnumerable<MasterBusinessActivity> GetBusinessActivity()
        {
             var bussinessAcvitity = FindBy(x => x.APP_Type == "1").OrderBy(x => x.ActivityName);
                return bussinessAcvitity;
           
        }

        // TODO: May be we need to Change this .
        /// <summary>
        /// This method is used to get the specific Business Activity for Anuglar and admin as well based on Activity Id.
        /// </summary>
        /// <returns>Specific Business Activity Data</returns>
        public IEnumerable<MasterBusinessActivity> FindByID(BusinessActivityModel businessActivityModel)
        {
             var bussinessAcvitity = FindBy(x => x.ActivityID == businessActivityModel.ActivityId && x.APP_Type == "1");
                return bussinessAcvitity;
           
        }

        /// <summary>
        /// This method is used to get the specific Business Activity for Anuglar and admin as well based on Activity Name .
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>Specific Business Activity Data</returns>
        public IEnumerable<MasterBusinessActivity> FindByActivityName(RenewModel renewModel)
        {
           
                var bussinessAcvitity = FindBy(x => x.ActivityName.Trim().Replace(System.Environment.NewLine, "").ToUpper()
                                .Contains(renewModel.ActivityName.ToUpper().Trim()));
                return bussinessAcvitity;
           
        }

        /// <summary>
        /// This method is used to get the specific Business Activity for Anuglar and admin as well based on Activity Id.
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns>Specific Business Activity Data</returns>
        public IEnumerable<MasterBusinessActivity> FindByActivityId(string activityId)
        {
         
                var bussinessAcvitity = FindBy(x => x.ActivityID == activityId && x.APP_Type == "1");
                return bussinessAcvitity;
           
        }

        /// <summary>
        /// This method is used to create or edit Business Activity through Admin Portal
        /// </summary>
        /// <param name="businessActivityModel"></param>
        /// <returns>Retrun result in Numbers</returns>
        public int InsertUpdateBusinessActivity(BusinessActivityEntity businessActivityModel)
        {
            int result = 0;
            try
            {
                string businessActivityName=Regex.Replace(businessActivityModel.ActivityName, @"\s+", " ");
                var businessactivities = GetAll().AsQueryable();
                var validate = businessactivities.Where(x => x.ActivityID == businessActivityModel.ActivityID);
                var activityname = businessactivities.Where(x => x.ActivityName.Trim().Replace(System.Environment.NewLine, "").ToUpper() ==
                  businessActivityName.Trim().ToUpper()).ToList();

                if (!validate.Any())
                {
                    if (!activityname.Any())
                    {
                        var businessActivity = new MasterBusinessActivity
                        {
                            ActivityID = Guid.NewGuid().ToString().Trim(),
                            ActivityName = businessActivityModel.ActivityName,
                            APP_Type = "1",
                            CreateDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        };
                        Add(businessActivity);
                        Save();
                        result = 1;
                    }
                    else
                    {
                        result = 3;
                    }
                }
                else
                {
                    if (!activityname.Any())
                    {
                        var activityName = validate.FirstOrDefault();
                        activityName.ActivityName = businessActivityModel.ActivityName;
                        activityName.APP_Type = businessActivityModel.APP_Type;
                        activityName.UpdatedDate = DateTime.Now;
                        Update(activityName, activityName.ActivityID);
                        Save();
                        result = 2;
                    }
                    else
                    {
                        if (validate.First().ActivityName.ToUpper() == businessActivityModel.ActivityName.ToUpper())
                        {
                            var activityName = validate.FirstOrDefault();
                            if (activityName.APP_Type.Trim() == businessActivityModel.APP_Type.Trim())
                            {
                                result = 4;
                            }
                            else
                            {
                                activityName.ActivityName = businessActivityModel.ActivityName;
                                activityName.APP_Type = businessActivityModel.APP_Type;
                                activityName.UpdatedDate = DateTime.Now;
                                Update(activityName, activityName.ActivityID);
                                Save();
                                result = 2;
                            }
                        }
                        else
                        {
                            result = 3;
                        }
                    }
                }
            }
            catch (Exception )
            {
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// This method is used to Deactivate a specific Business Activity based on Activity Id
        /// </summary>
        /// <param name="businessActivityModel"></param>
        /// <returns>Return status in bool</returns>
        public bool DeleteBusinessActivity(BusinessActivityEntity businessActivityModel)
        {
            bool status = false;
            try
            {
                var businessActivity = FindBy(x => x.ActivityID == businessActivityModel.ActivityID).Single();
                businessActivity.APP_Type = businessActivityModel.APP_Type;//"0";//
                Update(businessActivity, businessActivityModel.ActivityID);
                Save();
                status = true;
            }
            catch (Exception )
            { status = false; }
            return status;
        }

       /// <summary>
       /// This method is used to get Activity based on activityID
       /// </summary>
       /// <param name="businessActivityModel"></param>
        /// <returns>Specific Business Activity Data</returns>
        public IEnumerable<MasterBusinessActivity> FindByIDBasedonActivityId(BusinessActivityModel businessActivityModel)
        {
              var bussinessAcvitity = FindBy(x => x.ActivityID == businessActivityModel.ActivityId);
                return bussinessAcvitity;
           
        }

        
    }
}