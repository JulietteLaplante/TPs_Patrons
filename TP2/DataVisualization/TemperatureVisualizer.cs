using System;
using System.Collections.Generic;
using System.Text;
using Reflection.Attributes;
using static Reflection.Enums.UnitEnum;
using static Reflection.Enums.TypeEnum;

namespace Reflection.DataVisualization
{
    [MyCustomDataVisualizerAttribute(DataType.TEMP)]
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
