using System;
using System.Collections.Generic;
using System.Text;
using static Reflection.Enums.TypeEnum;

namespace Reflection.Attributes
{
    public class MyCustomDataVisualizerAttribute : Attribute
    {
        public DataType type;
        public MyCustomDataVisualizerAttribute(DataType t)
        {
            this.type = t;
        }
    }
}
