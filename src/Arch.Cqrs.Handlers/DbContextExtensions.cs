using Arch.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Arch.Cqrs.Handlers
{
    public static class DbContextExtensions
    {
        public static string AddEntity(this DbContext context, Entity entity)
        {
            context.Add(entity);
            return $"{entity.GetType().Name} Created";
        }

        public static string UpdateEntity(this DbContext context, Entity entity) =>
             $"{entity.GetType().Name} Updated";

        public static string DeleteEntity(this DbContext context, Entity entity)
        {
            context.Remove(entity);
            return $"{entity.GetType().Name} Deleted";
        }
    }
}
