using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Common
{
    public interface IUnitOfWork : IDisposable
    {

        /// <summary>
        /// Begins a Transaction By creating the transaction object
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Rollbacks a particular comitted transaction
        /// </summary>
        void RollBack();
        DbContext getContext();
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit();
    }
}
