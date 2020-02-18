using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Reflection.Attributes;
using static Reflection.TypeEnum;
using static Reflection.UnitEnum;

namespace Reflection
{
    class SensorManager
    {
        IList<Sensor> sensors;
        IList<DataVisualizer> visualizers;

        public SensorManager()
        {
            this.sensors = new List<Sensor>();
            this.visualizers = new List<DataVisualizer>();
        }

        public void AddSensor(Sensor s)
        {
            Unit sensorUnit;
            DataType sensorType;

            sensors.Add(s);
            MemberInfo info = s.GetType();
            object[] attributes = info.GetCustomAttributes(false);
            foreach(object o in attributes)
            {
                if(o is MyCustomSensorAttribute)
                {
                    sensorUnit = ((MyCustomSensorAttribute)o).unit;
                    sensorType = ((MyCustomSensorAttribute)o).type;
                    break;
                } else
                {
                    Console.WriteLine("attribute is of type :" + o.ToString());
                }
            }
            Console.WriteLine("On a récupéré le type et l'unit du sensor");
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            foreach(Type t in currentAssembly.GetTypes())
            {
                MemberInfo typeInfo = t;
                attributes = info.GetCustomAttributes(false);
                foreach(object o in attributes)
                {
                    if (o is MyCustomDataVisualizerAttribute)
                    {
                        // alors t est une class de datavizualization
                        // si l'attribut a comme valeur de type la meme que celle du sensor
                        // alors on l'instantie avec la bonne unitée
                        // et on l'ajoute a la liste
                    }
                }
            }







        }
    }
}
