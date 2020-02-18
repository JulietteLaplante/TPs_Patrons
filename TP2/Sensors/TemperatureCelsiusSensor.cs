using System;
using System.Collections.Generic;
using System.Text;
using static Reflection.UnitEnum;
using static Reflection.TypeEnum;

namespace Reflection
{
    [MyCustomSensorAttribute(DataType.TEMP, Unit.CELSIUS)]
    class TemperatureCelsiusSensor : Sensor
    {
        public float Sense()
        {
            return 10;
        }
    }
}
