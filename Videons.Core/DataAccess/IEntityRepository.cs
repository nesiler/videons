using System.Linq.Expressions;
using Videons.Core.Entities;

namespace Videons.Core.DataAccess;

public interface IEntityRepository<T> where T: EntityBase, new() //type argumani EntityBase den tureyen ve parametre icermeyen bir constructora sahip olmalidir.
{
    T Get(Expression<Func<T, bool>> filter);
    
    IList<T> GetList(Expression<Func<T, bool>> filter = null);
    bool Add(T entity);
    bool Update(T entity);
    bool Delete(T entity);
}