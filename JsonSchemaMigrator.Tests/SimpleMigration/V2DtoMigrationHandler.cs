using System;

namespace JsonSchemaMigrator.Tests
{
    public class V2DtoMigrationHandler : IMigrationHandler<V2Dto>
    {
        public Type UpgradeTargetType => typeof(V3Dto);

        public Type DowngradeTargetType => null;

        public object Upgrade(V2Dto source)
        {
            return new V3Dto(source.IntProperty, source.NewStringProp);
        }

        public object Downgrade(V2Dto source)
        {
            throw new NotImplementedException();
        }
    }
}
