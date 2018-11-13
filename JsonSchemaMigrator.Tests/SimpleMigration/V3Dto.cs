using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSchemaMigrator.Tests
{
    public class V3Dto
    {
        public int IntProp{ get; }

        public string NewStringProp { get; protected set; }

        public V3Dto(int intProp, string newStringProp)
        {
            IntProp = intProp;
            NewStringProp = newStringProp;
        }
    }
}
