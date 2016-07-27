using System;
using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionToAccelaRepository : GenericRepository<SubmissiontoAccela>, ISubmissionToAccelaRepository
    {
        public SubmissionToAccelaRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to get all submission accela data by descending order submssion accela id
        /// </summary>
        /// <returns>Retrun submission accela</returns>
        public IEnumerable<SubmissiontoAccela> GetSubmissionsToAccela()
        {
            var submissions = GetAll().AsQueryable().OrderByDescending(x=>x.SubmissiontoAccelaId);
            return submissions;
        }
        /// <summary>
        /// This method is to insert submission accela based on submision to accela entity
        /// </summary>
        /// <param name="submissiontoAccelaEntity"></param>
        /// <returns>Return bool data</returns>
        public bool AddSubmissionToAccelaRepository(SubmissiontoAccelaEntity submissiontoAccelaEntity)
        {
            try
            {
                var addToAccela = new SubmissiontoAccela();
                addToAccela.InjectFrom(submissiontoAccelaEntity);
                Add(addToAccela);
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