using System;
using System.Collections.Generic;
using System.Text;
using static Reflection.Enums.TypeEnum;
using static Reflection.Enums.UnitEnum;

namespace Reflection.DataVisualization
{
    interface DataVisualizer
    {
        void Display(float value);
    }
}
