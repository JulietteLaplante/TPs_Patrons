using Reflection.Sensors;
using System;

namespace Reflection
{
    class Program
    {

        static void Main(string[] args)
        {
            Sensor s1 = new TemperatureCelsiusSensor();
            Sensor s2 = new TemperatureFahrenheitSensor();

            SensorManager sm = new SensorManager();

            sm.AddSensor(s1);
            sm.AddSensor(s2);

            sm.Sense();

        }
    }
}
