using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Implementation
{
    public class DocumentsView4Service : IDocumentsView4Service
    {
        private readonly IDocumentsView4Repository _dcDocumentsView4Repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="dcDocumentsView4Repository"></param>
        public DocumentsView4Service(IDocumentsView4Repository dcDocumentsView4Repository)
        {
            _dcDocumentsView4Repository = dcDocumentsView4Repository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IQueryable<BblLicenseView4> GetBblLicenseView4()
        {
            var commandata = _dcDocumentsView4Repository.GetBblLicenseView4();
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentData"></param>
        /// <returns></returns>
        public IEnumerable<BblLicenseView4> FindByFileNumber(DocumentData documentData)
        {
            var commandata = _dcDocumentsView4Repository.FindByFileNumber(documentData);
            return commandata;
        }
    }
}