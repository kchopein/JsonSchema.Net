using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSchemaMigrator.AutofacProvider
{
    public class AutofactMigrationHandlerProvider : IMigrationHandlerProvider
    {
        public IMigrationHandler<TSource> GetMigrationHandler<TSource>()
        {
            throw new NotImplementedException();
        }
    }
}
