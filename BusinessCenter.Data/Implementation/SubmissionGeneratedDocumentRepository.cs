using System;
using System.Linq;
using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using Omu.ValueInjecter;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionGeneratedDocumentRepository : GenericRepository<SubmissionGeneratedDocument>, ISubmissionGeneratedDocumentRepository
        
    {
        public SubmissionGeneratedDocumentRepository(IUnitOfWork context)
            : base(context)
        {
           

        }
        /// <summary>
        /// This method is used to check  specific submission generated document exist or not based on unique id and document type.
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="documentType"></param>
        /// <returns>Return bool value</returns>
        public bool FindDocument(string masterId, string documentType)
        {
            return FindBy(x => x.MasterId.Trim() == masterId.Trim() && x.Gen_DocumentFrom.Trim().ToUpper() == documentType.Trim().ToUpper()).Any();
        }
        /// <summary>
        /// This method is used to Insert submission generated document based in user inputs
        /// </summary>
        /// <param name="businessEntityGenarationPdfEntity"></param>
        /// <returns>Return bool value</returns>
        public bool AddBusinessEntityGenarationPdf(SubmissionGeneratedDocumentEntity businessEntityGenarationPdfEntity)
        {
            try
            {
                var checkExitstedPdf = FindBy(x => x.MasterId == businessEntityGenarationPdfEntity.MasterId
                    && x.UserId == businessEntityGenarationPdfEntity.UserId && x.Gen_DocumentFrom == businessEntityGenarationPdfEntity.Gen_DocumentFrom).ToList();
                if (!checkExitstedPdf.Any())
                {
                var addToBusinessEntityGenarationPdf = new SubmissionGeneratedDocument();
                addToBusinessEntityGenarationPdf.InjectFrom(businessEntityGenarationPdfEntity);
                Add(addToBusinessEntityGenarationPdf);
                Save();
                return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method is used to get the submission generated documents based on unique id , user id and document from
        /// </summary>
        /// <param name="masterid"></param>
        /// <param name="userId"></param>
        /// <param name="documentFrom"></param>
        /// <returns>Return SubmissionGeneratedDocumentData </returns>
        public SubmissionGeneratedDocumentData AddBusinessEntityGenarationPdf(string masterid, string userId,string documentFrom)
        {
            SubmissionGeneratedDocumentData getResult = new SubmissionGeneratedDocumentData();

            try
            {
               
                var getBytCode = FindBy(x => x.MasterId == masterid && x.UserId == userId && x.Gen_DocumentFrom == documentFrom).ToList();
                if (getBytCode.Count() != 0)
                {
                    getResult.FileBytes =getBytCode.FirstOrDefault().FileByteCode;
                    getResult.Filename = getBytCode.FirstOrDefault().FileType;
                }

                return getResult;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}