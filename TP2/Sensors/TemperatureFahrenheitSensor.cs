using System;
using System.Collections.Generic;
using System.Text;
using static Reflection.TypeEnum;
using static Reflection.UnitEnum;

namespace Reflection
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
