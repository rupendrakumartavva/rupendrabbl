using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Implementation
{
    public class Portal_Content_ErrorsRepository : GenericRepository<Portal_Content_Errors>, IPortal_Content_ErrorsRepository
    {
        public Portal_Content_ErrorsRepository(IUnitOfWork context)
            :base(context)
        { 
        
        }
        /// <summary>
        /// This method is used get all portal content errors list
        /// </summary>
        /// <returns>Return Portal_Content_Errors data</returns>
        public IEnumerable<Portal_Content_Errors> GetAllErrors()
        {
            var contentErrors = GetAll().AsEnumerable();
            return contentErrors;
        }
        /// <summary>
        /// This method is used to get specific portal content error based ib message id
        /// </summary>
        /// <param name="portalContentErrors"></param>
        /// <returns>Return Portal_Content_Errors data</returns>
        public IEnumerable<Portal_Content_Errors> FindByMessageId(PortaContentErrorsModel portalContentErrors)
        {
            var contentErrors = FindBy(x => x.MessageId.Trim() == portalContentErrors.MessageId.Trim()).AsEnumerable();
            return contentErrors;
        }
        /// <summary>
        /// This method is used to get specific Portal_Content_Errors based on user inputs
        /// </summary>
        /// <param name="portalContentErrors"></param>
        /// <returns>Return Portal_Content_Error data</returns>
        public IEnumerable<Portal_Content_Errors> FindByMessageName(PortaContentErrorsModel portalContentErrors)
        {
            var contentErrors = FindBy(x => x.MessageType.Trim() == portalContentErrors.MessageType.Trim() && x.IsActive == true
                && x.ShortName.Replace(System.Environment.NewLine, "").Trim().ToUpper() == portalContentErrors.ShortName.Trim().ToUpper()).AsEnumerable();
            return contentErrors;
        }
        /// <summary>
        /// This method is used to insert/update Portal_Content_Errors data based on user inputs
        /// </summary>
        /// <param name="portalContentErrors"></param>
        /// <returns>Return bool value</returns>
        public bool InsertUpdateContentErrors(PortaContentErrorsModel portalContentErrors)
        {
            try
            {
              
                var contentErrors = GetAll().AsQueryable();
                var validate = contentErrors.Where(x => x.MessageId == portalContentErrors.MessageId);
                var errorMessage = contentErrors.Where(x => x.MessageType.Trim().ToUpper() == portalContentErrors.MessageType.Trim().ToUpper()
                    && x.ShortName.Replace(System.Environment.NewLine, "").ToUpper().Trim() == portalContentErrors.ShortName.ToUpper().Trim()).ToList();
                if (!validate.Any())
                {
                    if (!errorMessage.Any())
                    {
                        var portalContent = new Portal_Content_Errors
                        {
                            MessageId = Guid.NewGuid().ToString(),
                            MessageType = (portalContentErrors.MessageType ?? "").Trim(),
                            ShortName = (portalContentErrors.ShortName ?? "").Trim(),
                            ErrrorMessage = (portalContentErrors.ErrrorMessage ?? "").Trim(),
                            IsActive = true,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        };
                        Add(portalContent);
                        Save();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (!errorMessage.Any())
                    {
                        var portalContentId = validate.FirstOrDefault();
                        portalContentId.ErrrorMessage = portalContentErrors.ErrrorMessage;
                        portalContentId.ShortName = portalContentErrors.ShortName;
                        portalContentId.MessageType = portalContentErrors.MessageType;
                        portalContentId.UpdatedDate = System.DateTime.Now;
                        Update(portalContentId, portalContentId.MessageId);
                        Save();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method is used to inactive specific Portal_Content_Errors based on message id
        /// </summary>
        /// <param name="portalContentErrors"></param>
        /// <returns>Return bool value</returns>
        public bool DeleteContentErrors(PortaContentErrorsModel portalContentErrors)
        {
            bool status = false;
            try
            {
                var portalcontent = (FindBy(x => x.MessageId == portalContentErrors.MessageId)).FirstOrDefault();
                portalcontent.IsActive = portalContentErrors.IsActive;//false;//
                Update(portalcontent, portalcontent.MessageId);
                Save();
                status = true;
            }
              catch (Exception )
            {
                status = false;
            }
            return status;
        }
    }
}
