using System;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessCenter.Data.Interface
{
    public interface IKeywordDetailsRepository
    {
        IQueryable<KeywordDetails> FindBy(Expression<Func<KeywordDetails, bool>> predicate);
        IQueryable<KeywordDetails> KeyDetailsGetAll();
        void AddKeyWord(KeywordDetails entity);
        void SaveChanges();
    }
}