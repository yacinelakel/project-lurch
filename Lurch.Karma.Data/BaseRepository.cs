using System.Data;

namespace Lurch.Karma.Data
{
    public abstract class BaseRepository
    {
        protected BaseRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        protected IDbConnection Connection => Transaction.Connection ;

        protected IDbTransaction Transaction { get; }
    }
}