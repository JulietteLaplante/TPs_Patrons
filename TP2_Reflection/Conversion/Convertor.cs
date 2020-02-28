using Reflection.DataVisualization;
using Reflection.Sensors;
using Reflection.Attributes;
using static Reflection.Enums.UnitEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP2.Conversion
{
    interface Convertor : Sensor, DataVisualizer
    {
        float Convert(float input);

        Sensor GetSensor();
    }
}
