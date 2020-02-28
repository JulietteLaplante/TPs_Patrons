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
    [MyCustomConvertorAttribute(Unit.FAHRENHEIT, Unit.CELSIUS)]
    class FahrenheitToCelsiusConvertor : Convertor
    {
        Sensor sensor;
        public FahrenheitToCelsiusConvertor(Sensor s)
        {
            this.sensor = s;
        }
        public void Display(float value)
        {
            throw new NotImplementedException();
        }

        public float Sense()
        {
            return Convert(sensor.Sense());
        }

        public float Convert(float input)
        {
            return (input - 32) / 1.8f;
        }

        public Sensor GetSensor()
        {
            if( sensor is Convertor)
            {
                return ((Convertor)sensor).GetSensor();
            }else
            {
                return sensor;
            }
        }
    }
}
