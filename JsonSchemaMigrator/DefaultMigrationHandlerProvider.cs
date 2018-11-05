using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSchemaMigrator
{
    public class DefaultMigrationHandlerProvider : IMigrationHandlerProvider
    {
        private Dictionary<Type, List<object>> migrationHandlers = new Dictionary<Type, List<object>>();

        public IEnumerable<IMigrationHandler<TSource>> GetMigrationHandlers<TSource>()
        {
            throw new NotImplementedException();
        }
    }
}
