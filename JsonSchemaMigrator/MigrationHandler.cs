using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSchemaMigrator
{
    public abstract class MigrationHandler<TSource> : IMigrationHandler<TSource>
    {
        public virtual Type UpgradeTargetType { get; } = null;

        public virtual Type DowngradeTargetType { get; } = null;

        public virtual object Downgrade(TSource source)
        {
            throw new NotImplementedException();
        }

        public virtual object Upgrade(TSource source)
        {
            throw new NotImplementedException();
        }
    }
}
