using System.Collections.Generic;

namespace JsonSchemaMigrator
{
    public interface IMigrationHandlerProvider
    {
        IMigrationHandler<TSource> GetMigrationHandler<TSource>();
    }
}