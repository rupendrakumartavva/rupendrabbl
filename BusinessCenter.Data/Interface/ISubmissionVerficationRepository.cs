using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
   public interface ISubmissionVerficationRepository
    {
       SubmissionVerfication SubmissionDetails(SubmissionVerfication subVerfication);
       SubmissionVerfication SubmissionPayDetails(SubmissionVerfication subVerfication);
       BasicBusinessLicense BusinessReceipt(BasicBusinessLicense basicBusinessLicense);
       RenewBasicBusinessLicense RenewBusinessReceipt(RenewBasicBusinessLicense renewBasicBusinessLicense);
       string GetStateFullName(string stateCode,string countryCode);
       string GetCountryFullName(string countryCode);
       string GetStateCode(string stateName, string countryCode);
    }
}
