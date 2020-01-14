using Arch.Infra.Shared.Cqrs.Query;
using System.Collections.Generic;

namespace Arch.CqrsClient.Query.Generics
{
    public class GetObjectsCsv : IQuery<IEnumerable<object>>
    {
        public string Properties { get; set; }
        public string Order { get; set; }
        public string ViewModelAssemblyFullName { get; set; }
        public string ModelAssemblyFullName { get; set; }
    }
}
