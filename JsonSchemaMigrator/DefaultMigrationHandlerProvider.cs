using System;
using System.Collections.Generic;

namespace JsonSchemaMigrator
{
    public class DefaultMigrationHandlerProvider : IMigrationHandlerProvider
    {
        private Dictionary<Type, object> migrationHandlers = new Dictionary<Type, object>();

        public IMigrationHandler<TSource> GetMigrationHandler<TSource>()
        {
            Type handlerType = typeof(TSource);
            if (migrationHandlers.ContainsKey(handlerType))
            {
                return (IMigrationHandler<TSource>)migrationHandlers[handlerType];
            }

            return null;
        }

        public void AddMigrationHandler<TSource>(IMigrationHandler<TSource> migrationHandler)
        {
            Type handlerType = typeof(TSource);
            migrationHandlers[handlerType] = migrationHandler;
        }

    }
}
