using System;
using System.Collections.Generic;
using System.Text;
using static Reflection.Enums.UnitEnum;

namespace Reflection.Attributes
{
    public class MyCustomConversionAttribute : Attribute
    {
        public Unit input;
        public Unit output;
        public MyCustomConversionAttribute(Unit input, Unit output)
        {
            this.input = input;
            this.output = output;
        }
    }
}
