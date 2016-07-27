using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
   public class SubmissionToAccelaRepositoryData
    {
       private readonly List<SubmissiontoAccela> _entities;


       public void SubmissiontoAccelaEntity(SubmissiontoAccela obj)
        {
            _entities.Add(obj);
        }

       public List<SubmissiontoAccela> SubmissiontoAccelaEntitiesList
        {
            get { return _entities; }
        }
       public SubmissionToAccelaRepositoryData()
        {
           
            _entities = new List<SubmissiontoAccela>();

            SubmissiontoAccelaEntity(new SubmissiontoAccela()
            {
                SubmissiontoAccelaId=1,
                LicenseNumber="10001987",
                ApplicationCompleted=true,
                ApplicationCreated=false,
                ApplicationFeeMatched = false,
                RenewalPaymentUpdated = false,
                RenewalFeeMatched = false,
                AllDocumentsUpdated = false,

            });
          
        }
    }
}
