using Reflection.Sensors;
using System;

namespace Reflection
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Creation d'un sensor qui renvoit toujours 10 degres Celsius.");
            Sensor s1 = new TemperatureCelsiusSensor(); // renvoit 10 degres celsius
            Console.WriteLine("Creation d'un sensor qui renvoit toujours 100 degres Fahrenheit.");
            Sensor s2 = new TemperatureFahrenheitSensor(); // renvoit 100 degres fahrenheit

            SensorManager sm = new SensorManager();

            sm.AddSensor(s1, false);
            sm.AddSensor(s2, false);

            Console.WriteLine("\nRelevé des sensors SANS convertisseurs:\n");
            sm.Sense();

            sm.RemoveSensor(s1);
            sm.RemoveSensor(s2);

            sm.AddSensor(s1);
            sm.AddSensor(s2);
            Console.WriteLine("\nRelevé des sensors AVEC convertisseurs:\n");
            sm.Sense();

        }
    }
}
