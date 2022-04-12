using System.Collections.Generic;
using System.Threading.Tasks;
using Diff.Application.Interfaces.Repositories.Base;
using Diff.Domain.Entities;

namespace Diff.Application.Interfaces.Repositories
{
    public interface IInputRepository : IRepository<InputModel>
    {
        Task<InputModel> GetPair(IEnumerable<InputModel> pairs, string side);
        Task<IList<InputModel>> GetPairs(int id);
        new Task<bool> Add(InputModel entity);
        void IsExist(int id, string side);
    }
}