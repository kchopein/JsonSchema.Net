using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSchemaMigrator.AutofacProvider
{
    public class AutofactMigrationHandlerProvider : IMigrationHandlerProvider
    {
        public IEnumerable<IMigrationHandler<TSource>> GetMigrationHandlers<TSource>()
        {
            throw new NotImplementedException();
        }
    }
}
