using Arch.Infra.Data;
using System.Collections.Generic;
using System.Linq;

namespace Arch.CqrsHandlers.Generic
{
    public class Repository<T> where T : class
    {
        public IEnumerable<T> GetAll(string order) => new ArchCoreContext()
             .Set<T>().ToList().OrderBy(_ => _.GetType().GetProperty(order).GetValue(_, null));
    }
}
