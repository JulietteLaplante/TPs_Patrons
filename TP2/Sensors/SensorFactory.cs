using Reflection.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reflection.Sensors
{
    public class SensorFactory
    {
        public Sensor CreateSensor(string model)
        {
            if(model == "StandardTemperature")
            {
                return new TemperatureCelsiusSensor(); // renvoit 10 degres celsius
            } else if (model == "ExoticTemperature")
            {
                return new TemperatureFahrenheitSensor(); // renvoit 100 degres fahrenheit
            }else
            {
                return null;
            }

        }
    }
}
