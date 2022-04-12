using System.Threading.Tasks;
using Diff.Application.Interfaces.Repositories.Base;
using Diff.Infrastructure.Data;

namespace Diff.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly InputContext<T> _inputContext;

        protected Repository(InputContext<T> inputContext)
        {
            _inputContext = inputContext;
        }

        public Task<bool> Add(T entity)
        {
            _inputContext.InputModels.Add(entity);
            return Task.FromResult(_inputContext.InputModels.Contains(entity));
        }

        public Task<bool> Remove(T entity)
        {
            return Task.FromResult(_inputContext.InputModels.Remove(entity));
        }
    }
}