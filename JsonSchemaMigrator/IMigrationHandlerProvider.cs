using System.Collections.Generic;

namespace JsonSchemaMigrator
{
    public interface IMigrationHandlerProvider
    {
        IEnumerable<IMigrationHandler<TSource>> GetMigrationHandlers<TSource>();
    }
}