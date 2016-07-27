using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionLicenseNumberCounterRepository : GenericRepository<SubmissionLicenseNumberCounter>, ISubmissionLicenseNumberCounterRepository
    {
        public SubmissionLicenseNumberCounterRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to Get specific counter data based on Type  
        /// </summary>
        /// <param name="submissioncounter"></param>
        /// <returns>Retrun SubmissionLicenseNumberCounter </returns>
        public IEnumerable<SubmissionLicenseNumberCounter> FindBy(SubmissionCounter submissioncounter)
        {
            var commondata = FindBy(x => x.Type.Replace(System.Environment.NewLine,"").ToUpper().Trim() == submissioncounter.Type.ToUpper()).ToList();
            return commondata;
        }
        /// <summary>
        /// This method is used to insert/update the submission counter based on type and year
        /// </summary>
        /// <param name="submissioncounter"></param>
        /// <returns>Return string value</returns>
        public string InsertUpdateSubmissionCounter(SubmissionCounter submissioncounter)
        {
            string counterId;
            var counterExist = FindBy(submissioncounter).ToList();
            if (counterExist.Count == 0)
            {
                SubmissionLicenseNumberCounter licensecounter = new SubmissionLicenseNumberCounter();
                if (submissioncounter.Type.ToUpper() ==GenericEnums.GetEnumDescription(GenericEnums.SubmissionCounter.DAPP).ToString())
                {
                    licensecounter.Counter = Convert.ToInt32(GenericEnums.GetEnumDescription(GenericEnums.SubmissionNumberCounter.InitialNumber));
                    //licensecounter.Sequence = Convert.ToInt32(GenericEnums.GetEnumDescription(GenericEnums.SubmissionNumberCounter.Sequence8));
                }
                else if (submissioncounter.Type.ToUpper() ==GenericEnums.GetEnumDescription(GenericEnums.SubmissionCounter.LAPP_IAPP).ToString())
                {
                    licensecounter.Counter = Convert.ToInt32(GenericEnums.GetEnumDescription(GenericEnums.SubmissionNumberCounter.InitialNumberStartWith8));
               
                }
                licensecounter.Sequence = Convert.ToInt32(GenericEnums.GetEnumDescription(GenericEnums.SubmissionNumberCounter.Sequence8));
                licensecounter.FiscalYear = submissioncounter.PhysicalYear;
                licensecounter.Type = submissioncounter.Type;
                Add(licensecounter);
                Save();
                counterId = licensecounter.CounterId.ToString();
            }
            else
            {
                var counterupdate = counterExist.First();
                if (submissioncounter.Type.ToUpper() == GenericEnums.SubmissionCounter.DAPP.ToString().ToUpper())
                {
                    if (counterupdate.Counter == Convert.ToInt32(GenericEnums.GetEnumDescription(GenericEnums.SubmissionNumberCounter.DdppHeightNumber)))
                    {
                        //
                        counterupdate.Counter = Convert.ToInt32(GenericEnums.GetEnumDescription(GenericEnums.SubmissionNumberCounter.InitialNumber));
                        counterupdate.Sequence = counterupdate.Sequence - 1;
                    }
                    else
                    { 
                        counterupdate.Counter = counterupdate.Counter + 1;
                        counterupdate.Sequence = counterupdate.Sequence;
                    }

                }
                else if (submissioncounter.Type.ToUpper() == GenericEnums.SubmissionCounter.LAPP_IAPP.ToString().ToUpper())
                 {
                     if (counterupdate.Counter == Convert.ToInt32(GenericEnums.GetEnumDescription(GenericEnums.SubmissionNumberCounter.LappHeightNumber)))
                     {
                         //
                         counterupdate.Counter = Convert.ToInt32(GenericEnums.GetEnumDescription(GenericEnums.SubmissionNumberCounter.InitialNumberStartWith8));
                         counterupdate.Sequence = counterupdate.Sequence - 1;
                     }
                     else
                     {
                         counterupdate.Counter = counterupdate.Counter + 1;
                         counterupdate.Sequence = counterupdate.Sequence;
                     }
                 }

                if (counterupdate.FiscalYear != submissioncounter.PhysicalYear)
                {
                    if (submissioncounter.Type.ToUpper() == GenericEnums.SubmissionCounter.DAPP.ToString().ToUpper())
                    {
                        counterupdate.Counter = Convert.ToInt32(GenericEnums.GetEnumDescription(GenericEnums.SubmissionNumberCounter.InitialNumber));
                        counterupdate.Sequence = Convert.ToInt32(GenericEnums.GetEnumDescription(GenericEnums.SubmissionNumberCounter.Sequence8));
                    }
                    else if (submissioncounter.Type.ToUpper() == GenericEnums.SubmissionCounter.LAPP_IAPP.ToString().ToUpper())
                    {
                        counterupdate.Counter =
                            Convert.ToInt32(
                                GenericEnums.GetEnumDescription(
                                    GenericEnums.SubmissionNumberCounter.InitialNumberStartWith8));
                        counterupdate.Sequence = Convert.ToInt32(GenericEnums.GetEnumDescription(GenericEnums.SubmissionNumberCounter.Sequence8));
                    }
                 
                }
                counterupdate.FiscalYear = submissioncounter.PhysicalYear;              
                counterupdate.Type = submissioncounter.Type;
                Update(counterupdate, counterupdate.CounterId);
                Save();
                counterId = counterupdate.CounterId.ToString();
            }
            return counterId;
        }
    }
}
