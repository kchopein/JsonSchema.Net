using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonSchemaMigrator
{
    internal class MigrationHandlerWrapper
    {
        private readonly Type MigrationHandlerType;
        private readonly object MigrationHandler;
        private readonly Type SourceType;

        public Type UpgradeTargetType { get; }
        public Type DowngradeTargetType { get; }

        internal MigrationHandlerWrapper(object migrationHandler)
        {
            this.MigrationHandlerType = migrationHandler.GetType();
            this.MigrationHandler = migrationHandler;
            UpgradeTargetType = MigrationHandlerType.GetProperty(nameof(IMigrationHandler<object>.UpgradeTargetType)).GetValue(MigrationHandler) as Type;
            DowngradeTargetType = MigrationHandlerType.GetProperty(nameof(IMigrationHandler<object>.DowngradeTargetType)).GetValue(MigrationHandler) as Type;
            SourceType = MigrationHandlerType.GetInterface(typeof(IMigrationHandler<>).Name).GetGenericArguments().Single();
        }

        public object Upgrade(object source)
        {
            var method = MigrationHandlerType.GetMethod(nameof(IMigrationHandler<object>.Upgrade), new Type[] { SourceType });
            var result = method.Invoke(MigrationHandler, new object[] { source });
            return result;
        }

        public object Downgrade(object source)
        {
            var method = MigrationHandlerType.GetMethod(nameof(IMigrationHandler<object>.Downgrade), new Type[] { SourceType });
            var result = method.Invoke(MigrationHandler, new object[] { source });
            return result;
        }
    }
}
