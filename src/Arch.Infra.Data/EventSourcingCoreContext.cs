using Arch.Domain.Core.Event;
using Arch.Infra.Shared.EventSourcing;
using Microsoft.EntityFrameworkCore;

namespace Arch.Infra.Data
{
    public class EventSourcingCoreContext : DbContext
    {
        public DbSet<StoredEvent> StoredEvent { get; set; }
        public DbSet<EventEntity> EventEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ArchDatabase4;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            optionsBuilder.UseSqlite("Data Source=ArchEventSourcingDatabase.db");
        }
    }
}
