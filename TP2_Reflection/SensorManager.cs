using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Reflection.Attributes;
using Reflection.DataVisualization;
using Reflection.Sensors;
using TP2.Conversion;
using static Reflection.Enums.TypeEnum;
using static Reflection.Enums.UnitEnum;

namespace Reflection.Sensors
{
    class SensorManager
    {
        IList<Sensor> sensors;
        IList<DataVisualizer> visualizers;
        IDictionary<Unit, Type> convertors;

        public SensorManager()
        {
            this.sensors = new List<Sensor>();
            this.visualizers = new List<DataVisualizer>();
            this.convertors = new Dictionary<Unit, Type>();
            this.ConvertorCreation();
        }
        public void RemoveSensor(Sensor sensorToRemove)
        {
            int i = 0;
            // s'arrete si :
            //      - on a parcouru tout les elements
            //      - on a trouvé le sensor a Remove
            //      - on a trouvé un convertisseur dont le sensor encapsulé est celui a Remove
            for (i = 0; i < sensors.Count && sensors[i] != sensorToRemove && !(sensors[i] is Convertor && sensorToRemove == ((Convertor)sensors[i]).GetSensor()); ++i) ;
            // Si la condition d'arret n'est pas que l'on a parcouru tout les elements
            // Alors on a trouvé le sensor a remove
            if (i != sensors.Count)
            {
                sensors.Remove(sensors[i]);
                visualizers.Remove(visualizers[i]);
            }
        }
        public void AddSensor(Sensor s, bool useConvertors=true)
        {
            Unit sensorUnit = Unit.BAR; // Arbitrary default value, won't be used
            DataType sensorType = DataType.TEMP; //  same
            Boolean error = true;

            // Récuperation du type et de l'unité du sensor (temperature/celius ou temperature/Fanrenheit ou pression/bar etc.)
            MemberInfo info = s.GetType();
            object[] attributes = info.GetCustomAttributes(false);
            foreach (object o in attributes)
            {
                if (o is MyCustomSensorAttribute)
                {
                    sensorUnit = ((MyCustomSensorAttribute)o).unit;
                    sensorType = ((MyCustomSensorAttribute)o).type;
                    error = false;
                    break;
                }
            }

            // On check si tout s'est bien passé
            if (error)
            {
                Console.WriteLine("La récuperation du type et de l'unité du sensor a échouée");
                return;
            }

            // Etape : Instantiation du Datavisualizer correspondant
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            // On parcours toutes les class de l'assembly courant ...
            foreach (Type t in currentAssembly.GetTypes())
            {
                MemberInfo typeInfo = t;
                attributes = typeInfo.GetCustomAttributes(false);
                // ... pour trouver leurs attributs custom non hérités ...
                foreach (object o in attributes)
                {
                    // ... et si cette classe possede l'attribut "MyCustomDataVisualizerAttribute" ... 
                    if (o is MyCustomDataVisualizerAttribute)
                    {
                        // ... et que cet attribut a un type (Temperature /pression etc) correspondant a celui du Sensor ... 
                        if (sensorType == ((MyCustomDataVisualizerAttribute)o).type)
                        {
                            // ... alors on a trouvé un DataVisualizer qui correspond !
                            
                            // Maintenant, est ce que l'unité utilisée par notre sensor possède un convertisseur correspondant ?
                            if( useConvertors && convertors.ContainsKey(sensorUnit))
                            {
                                // Si oui, il faut ajouter a la liste des sensors le convertisseur qui encapsule notre sensor

                                // on instantie le converteur
                                Type[] convertorConstructorTypes = new Type[1];
                                object[] convertorConstructorParameters = new object[1];

                                convertorConstructorTypes[0] = s.GetType();
                                convertorConstructorParameters[0] = s;

                                object c = convertors[sensorUnit].GetConstructor(convertorConstructorTypes).Invoke(convertorConstructorParameters);
                                sensors.Add((Sensor)c);

                                // L'unité du sensor est maintenant encapsulé dans le convertisseur
                                // On met donc a jour la variable sensorUnit pour que le datavisualizer soit crée avec la nouvelle unité (celle en sortie du convertisseur)

                                foreach(object attribute in convertors[sensorUnit].GetCustomAttributes(false))
                                {  
                                    if( attribute is MyCustomConvertorAttribute)
                                    {
                                        sensorUnit = ((MyCustomConvertorAttribute)attribute).output;
                                    }

                                }

                            } else
                            {
                                // Sinon on ajoute le sensor donné en paramètre sans l'encapsuler
                                sensors.Add(s);
                               
                            }

                            // Puis on instantie le DataVisualizer
                            Type[] dvConstructorTypes = new Type[1];
                            object[] dvConstructorParameters = new object[1];

                            dvConstructorTypes[0] = sensorUnit.GetType();
                            dvConstructorParameters[0] = sensorUnit;

                            // On appel le constructeur du DataVisualizer que l'on a trouvé
                            // On donne les type d'argument que prend le constructeur pour trouver le bon constructeur s'il y en a plusieurs
                            object dv = t.GetConstructor(dvConstructorTypes).Invoke(dvConstructorParameters);
                            visualizers.Add((DataVisualizer)dv);
                        }
                    }
                }
            }
        }

        public void Sense()
        {
            for (int i = 0; i < sensors.Count; ++i)
            {
                visualizers[i].Display(sensors[i].Sense());
            }
        }

        private void ConvertorCreation()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            // On parcours toutes les class de l'assembly courant ...
            foreach (Type t in currentAssembly.GetTypes())
            {
                MemberInfo typeInfo = t;
                object[] attributes = typeInfo.GetCustomAttributes(false);
                // ... pour trouver leurs attributs custom non hérités ...
                foreach (object o in attributes)
                {
                    // ... et si cette classe possede l'attribut "MyCustomConvertorAttribute" ... 
                    if (o is MyCustomConvertorAttribute)
                    {
                        // On ajoute au dictionnaire le type du converteur que l'on a trouver
                        convertors.Add(((MyCustomConvertorAttribute)o).input, t);
                    }

                }
            }
        }
    }
}
