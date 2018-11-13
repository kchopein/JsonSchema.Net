using System.Collections.Generic;
using System.Text;

namespace JsonSchemaMigrator.Tests
{
    public class V1Dto 
    {
        public static string NewStringProp = "Default";

        public int IntProperty { get; protected set; }
        public string StringProperty { get; protected set; }

        public V1Dto(int intProperty, string stringProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
        }
    }

}
