using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSchemaMigrator.Tests
{
    public class V2Dto : IUpgradeSchema<V3Dto>
    {
        public int IntProperty { get; protected set; }
        public string StringPropertyRenamed { get; protected set; }
        public string NewStringProp { get; protected set; }

        public V2Dto(int intProperty, string stringProperty, string newStringProp)
        {
            IntProperty = intProperty;
            StringPropertyRenamed = stringProperty;
            NewStringProp = newStringProp;
        }

        public V3Dto UpgradeTo()
        {
            return new V3Dto(IntProperty, NewStringProp);
        }
    }
}
