using System;
using System.Collections.Generic;
using System.Text;
using static Reflection.TypeEnum;

namespace Reflection.Attributes
{
    public class MyCustomDataVisualizerAttribute
    {
        public DataType type;
        public MyCustomDataVisualizerAttribute(DataType t)
        {
            this.type = t;
        }
    }
}
