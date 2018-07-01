using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSchemaMigrator.Tests
{
    public class V1Dto : IUpgradeSchema<V2Dto>
    {
        public int IntProperty { get; protected set; }
        public string StringProperty { get; protected set; }

        public V1Dto(int intProperty, string stringProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
        }

        public V2Dto UpgradeTo()
        {
            return new V2Dto(IntProperty, StringProperty, string.Empty);
        }
    }
}
