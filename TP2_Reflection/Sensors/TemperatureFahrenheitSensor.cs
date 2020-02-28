using Reflection.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using static Reflection.Enums.TypeEnum;
using static Reflection.Enums.UnitEnum;

namespace Reflection.Sensors
{
   [MyCustomSensorAttribute(DataType.TEMP, Unit.FAHRENHEIT)]
    class TemperatureFahrenheitSensor : Sensor
    {
        public float Sense()
        {
            return 100f;
        }
    }
}
