using Arch.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Arch.Cqrs.Handlers
{
    public static class DbContextExtensions
    {
        public static string AddEntity(this DbContext context, Entity entity)
        {
            context.Add(entity);
            return $"New {entity.GetType().Name}";
        }

        public static string UpdateEntity(this DbContext context, Entity entity)
        {
            return $"Updated {entity.GetType().Name}";
        }

        public static string DeleteEntity(this DbContext context, Entity entity)
        {
            context.Remove(entity);
            return $"Deleted {entity.GetType().Name}";
        }
    }
}
