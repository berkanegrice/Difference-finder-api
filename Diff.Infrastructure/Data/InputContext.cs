using System.Collections.Generic;

namespace Diff.Infrastructure.Data
{
    public class InputContext<T>
    {
        protected internal IList<T> InputModels { get; } = new List<T>();
    }
}
