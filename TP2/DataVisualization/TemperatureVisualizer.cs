using System;
using System.Collections.Generic;
using System.Text;
using static Reflection.UnitEnum;

namespace Reflection
{

    public class TemperatureVisualizer : DataVisualizer
    {
        Unit unit;
        public TemperatureVisualizer(Unit u)
        {
            this.unit = u;
        }


        public void Display(float value)
        {
            Console.WriteLine("Temperature: " + value + " " +  unit.ToString());
        }

    }
}
