using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Reflection.Attributes;
using Reflection.DataVisualization;
using Reflection.Sensors;
using static Reflection.Enums.TypeEnum;
using static Reflection.Enums.UnitEnum;

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
            Unit sensorUnit = Unit.BAR; // Arbitrary default value, won't be used
            DataType sensorType= DataType.TEMP; //  same
            Boolean error = true;
            sensors.Add(s);
            
            // Récuperation du type et de l'unité du sensor (temperature/celius ou temperature/Fanrenheint ou pression/bar etc.)
            MemberInfo info = s.GetType();
            object[] attributes = info.GetCustomAttributes(false);
            foreach(object o in attributes)
            {
                if(o is MyCustomSensorAttribute)
                {
                    sensorUnit = ((MyCustomSensorAttribute)o).unit;
                    sensorType = ((MyCustomSensorAttribute)o).type;
                    error = false;   
                    break;
                } else
                {
                    Console.WriteLine("attribute is of type :" + o.ToString());
                }
            }

            // On check si tout s'est bien passé
            if (error)
            {
                Console.WriteLine("La récuperation du type et de l'unité du sensor a échouée");
                return;
            } else
            {
                Console.WriteLine("On a récupéré le type et l'unit du sensor");
            }


            // Instantiation du Datavisualizer correspondant
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            foreach(Type t in currentAssembly.GetTypes())
            {
                MemberInfo typeInfo = t;
                attributes = typeInfo.GetCustomAttributes(false);
                foreach(object o in attributes)
                {
                    if (o is MyCustomDataVisualizerAttribute)
                    {
                        if (sensorType == ((MyCustomDataVisualizerAttribute)o).type)
                        {
                            Console.WriteLine("On a trouvé un DataVisualizer qui correspond a notre Sensor !");
                            Type[] constructorTypes = new Type[1];
                            object[] constructorParameters = new object[1];

                            constructorTypes[0] = sensorUnit.GetType();
                            constructorParameters[0] = sensorUnit;

                            // On appel le constructeur du DataVisualizer que l'on a trouvé
                            // On donne le type d'argument que prend le constructeur pour trouver le bon constructeur s'il y en a plusieurs
                            object dv = t.GetConstructor(constructorTypes).Invoke(constructorParameters);
                            visualizers.Add((DataVisualizer)dv);
                        }
                    }
                }
            }







        }
    }
}
