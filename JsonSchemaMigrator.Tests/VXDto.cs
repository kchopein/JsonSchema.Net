using System;
using System.Collections.Generic;
using System.Text;

namespace JsonSchemaMigrator.Tests
{
    public class VXDto
    {
        public int IntProp{ get; }

        public string NewStringProp { get; protected set; }

        public VXDto(int intProp, string newStringProp)
        {
            IntProp = intProp;
            NewStringProp = newStringProp;
        }

    }
}
