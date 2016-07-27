using System;
using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using Omu.ValueInjecter;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionDocumentToAccelaRepository : GenericRepository<SubmissionDocumentToAccela>, ISubmissionDocumentToAccelaRepository
    {
          public SubmissionDocumentToAccelaRepository(IUnitOfWork context)
            : base(context)
          {
          }

        /// <summary>
        /// This method is used to insert Required document to Accele Table based on User Inputs. 
        /// </summary>
        /// <param name="submissiontoAccelaEntity"></param>
        /// <returns>Retrun Bool Result</returns>
          public bool AddSubmissionDocumentsToAccelaRepository(SubmissionDocumentToAccelaEntity submissiontoAccelaEntity)
          {
              try
              {
                  var addToAccela = new SubmissionDocumentToAccela();
                  addToAccela.InjectFrom(submissiontoAccelaEntity);
                  Add(addToAccela);
                  Save();
                  return true;
              }
              catch (Exception )
              {
                  return false;
                 // throw new Exception("",ex);
              }

          }
        /// <summary>
        /// This method is used to insert document to accela based on user inputs
        /// </summary>
        /// <param name="accelaDocument"></param>
        /// <returns>Return bool value</returns>
          public bool UpdateFinalDocumentsToAccela(AccelaDocument accelaDocument)
          {
              try
              {
                  var subAccelaEntity = new SubmissionDocumentToAccela
                  {
                      SubmisionDocToAccelaId = 0,
                      LicenseNumber = (accelaDocument.LicenseNumber ?? "").Trim(),
                      MasterId = accelaDocument.MasterId,
                      MasterCategoryDocId = Convert.ToInt32(accelaDocument.MasterCategoryDocId),
                      FileName = (accelaDocument.FileName ?? "").Trim(),
                      CreatedDate = DateTime.Now,
                              Status = false

                          };
                         Add(subAccelaEntity);
                          Save();
                  return true;
                
              }
              catch (Exception)
              {
                  return false;
              }
          }
    }
}