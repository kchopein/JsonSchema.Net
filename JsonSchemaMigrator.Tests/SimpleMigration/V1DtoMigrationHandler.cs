using System;

namespace JsonSchemaMigrator.Tests
{
    public class V1DtoMigrationHandler : IMigrationHandler<V1Dto>
    {
        public Type UpgradeTargetType => typeof(V2Dto);

        public Type DowngradeTargetType => null;

        public object Upgrade(V1Dto source) 
        {
            return new V2Dto(source.IntProperty, source.StringProperty, V1Dto.NewStringProp);
        }

        public object Downgrade(V1Dto source)
        {
            throw new NotImplementedException();
        }
    }

}
