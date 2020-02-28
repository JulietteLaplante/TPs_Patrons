using System;
using System.Collections.Generic;
using System.Text;
using static Reflection.Enums.TypeEnum;
using static Reflection.Enums.UnitEnum;


namespace Reflection.Attributes
{
    public class MyCustomSensorAttribute : Attribute
    {
        public Unit unit;
        public DataType type;


        public MyCustomSensorAttribute(DataType t, Unit u)
        {
            this.type = t;
            this.unit = u;
        }
    }
}
