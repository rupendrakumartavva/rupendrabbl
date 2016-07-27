using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionCorporationRepository
    {
        IEnumerable<SubmissionCorporation_Agent> GetAllCorporations();
        IEnumerable<SubmissionCorporation_Agent> FindByMasterId(GeneralBusiness generalBusiness);
        bool InsertCorporationDetails(GeneralBusiness detailsModel);
        IEnumerable<GeneralBusiness> GetCorpBusinessData(GeneralBusiness generalBusiness);
        GeneralBusiness GetHQAddess(GeneralBusiness detailsModel);
        List<GeneralBusiness> GetCorpAgent(GeneralBusiness generalBusiness);
        IEnumerable<SubmissionCorporation_Agent> FindById(string MasterId);
        bool DeleteSubmissionCorp(string masterId);
        void ChcekListUpdateStatus(string masterId, string userSubType, bool status);
        bool DeleteSubmissionCorpEmpty(string masterId,string type);
        CorporationStatus CorpServiceStatus(string masterId);
        bool UpdateCorportationData(GeneralBusiness generalBusiness);
        void UpdateCorpDisplayStatus(GeneralBusiness generalBusiness);
        string CorpOnlineSearch(CorporationDetails corporationdetails);
    }
}
