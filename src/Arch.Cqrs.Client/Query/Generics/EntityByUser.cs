using System;
using System.ComponentModel;

namespace Arch.Cqrs.Client.Query.Generics
{
    public class EntityByUser
    {
        [DisplayName("Agrégé Id")]
        public Guid AggregatedId { get; set; }
        public string Action { get; set; }
        [DisplayName("Qui")]
        public string Who { get; set; }
        [DisplayName("Quand")]
        public string When { get; set; }
    }
}
