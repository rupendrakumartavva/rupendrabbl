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
    public class DocumentsView4Repository : GenericRepository<BblLicenseView4>, IDocumentsView4Repository
    {
        public DocumentsView4Repository(IUnitOfWork context)
            : base(context)
        { }
        /// <summary>
        /// This method is used to Get All BblLicenseView.
        /// </summary>
        /// <returns>Retrun BblLicenseView</returns>
        public IQueryable<BblLicenseView4> GetBblLicenseView4()
        {
            return GetAll().OrderByDescending(x => x.APPID).AsQueryable().Distinct();
        }
        /// <summary>
        /// This method is to retrive BblLicenseView based on Application ID and order by Updated_date
        /// </summary>
        /// <param name="documentData"></param>
        /// <returns>Retrun BblLicenseView</returns>
        public IEnumerable<BblLicenseView4> FindByFileNumber(DocumentData documentData)
        {
            int appid = Convert.ToInt32(documentData.ApplicationNo);
            var ViewData = FindBy(x => x.APPID == appid).OrderBy(x => x.UpDated_Date).AsEnumerable();
            return ViewData;
        }
    }
}
