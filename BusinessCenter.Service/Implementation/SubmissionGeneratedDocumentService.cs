using BusinessCenter.Common;
using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;
using System;

namespace BusinessCenter.Service.Implementation
{
    public class SubmissionGeneratedDocumentService : ISubmissionGeneratedDocumentService
    {
        protected ISubmissionGeneratedDocumentRepository BusinessEntityGenarationPdfRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="businessEntityGenarationPdfRepository"></param>
        public SubmissionGeneratedDocumentService(ISubmissionGeneratedDocumentRepository businessEntityGenarationPdfRepository)
        {
            BusinessEntityGenarationPdfRepository = businessEntityGenarationPdfRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="businessEntityGenarationPdfEntity"></param>
        /// <returns></returns>
        public bool AddBusinessEntityGenarationPdf(SubmissionGeneratedDocumentEntity businessEntityGenarationPdfEntity)
        {
            try
            {
                return
                    BusinessEntityGenarationPdfRepository.AddBusinessEntityGenarationPdf(
                        businessEntityGenarationPdfEntity);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <param name="userId"></param>
        /// <param name="documentFrom"></param>
        /// <returns></returns>
        public SubmissionGeneratedDocumentData BusinessEntityGenarationPdf(string licenseNumber, string userId, string documentFrom)
        {
            try
            {
                return
                    BusinessEntityGenarationPdfRepository.AddBusinessEntityGenarationPdf(
                        licenseNumber, userId, documentFrom);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public bool FindBblOrderDocuments(string masterId, string documentType)
        {
            var commandata = BusinessEntityGenarationPdfRepository.FindDocument(masterId, documentType);
            return commandata;
        }
    }
}