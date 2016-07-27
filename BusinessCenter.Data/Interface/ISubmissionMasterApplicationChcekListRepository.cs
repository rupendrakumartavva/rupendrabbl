using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionMasterApplicationChcekListRepository
    {
        IEnumerable<SubmissionMaster_ApplicationCheckList> FindByMasterId(string masterid);
        bool UpdateAllCheckListConditions(CofoHopDetailsModel cofoHopDetailsModel);
        bool UpdateIsCofo(CofoHopDetailsModel cofoHopDetailsModel);
        bool InsertSubmissionChecklist(SubmissionApplication submissionApp);
        SubmissionMaster_ApplicationCheckList UpdateCheckListApp(SubmissionTaxRevenue taxRevenu, bool validate);
        bool UpdateDetails(CofoHopDetailsModel generalbusiness);
       bool UpdateIsCorporation(GeneralBusiness detailsModel);
       bool UpdateIsCorporationAgent(GeneralBusiness detailsModel);
        bool UpdateIsMailAddress(GeneralBusiness detailsModel);
        bool ChcekListUpdateStatus(string masterId, string submittedType, bool updateSubmittedValue);
        bool UpdateCofodetails(CofoHopDetailsModel cofoModel);
        bool InsertRenewChecklist(RenewModel renewModel);
        bool UpdateCorpCheckStatus(string MasterId, string submittedType, string mailtype);
        bool UpdateMailStatus(string masterId);
        void CofoServiceStatus(string Masterid, string mailtype);
        void TaxServiceStatus(string Masterid, bool UpdateStatus);
        bool UpdateMailTrueStatus(string masterId);
        bool UpdateCorpSearchStatus(string masterid);
        bool DeleteSubmissionCheckList(string masterId);
       void UpdateChcekList(string submittedType, bool updateStatus, string masterId);
       bool UpdateCorpsHqstatusandagentStatus(string masterId, string submittedType);
    }
}
