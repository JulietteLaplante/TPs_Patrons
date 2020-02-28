using System;
using System.Collections.Generic;
using System.Text;
using static Reflection.Enums.UnitEnum;
using static Reflection.Enums.TypeEnum;
using Reflection.Attributes;

namespace Reflection.Sensors
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
