using System.Collections.Generic;
using System.Threading.Tasks;
using Diff.Domain.Entities;
using Diff.Domain.Repositories.Base;

namespace Diff.Domain.Repositories
{
    public interface IInputRepository : IRepository<InputModel>
    {
        Task<InputModel> GetPair(IEnumerable<InputModel> pairs, string side);
        Task<IList<InputModel>> GetPairs(int id);
        new Task<bool> Add(InputModel entity);
        void IsExist(int id, string side);
    }
}