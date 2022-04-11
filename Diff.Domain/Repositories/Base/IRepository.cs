using System.Threading.Tasks;

namespace Diff.Domain.Repositories.Base
{
    public interface IRepository<in T> where T : class
    {
        Task<bool> Add(T entity);
        Task<bool> Remove(T entity);
    }
}