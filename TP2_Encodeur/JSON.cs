using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace EncodeurJSON
{
    public class JSON
    {
        public static Dictionary<string, object> Serialize(object obj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            // Obtention of the field infos of the object
            var infos = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (var info in infos)
            {
                // Content of the field
                object value = info.GetValue(obj);

                if (value.GetType().IsClass && !value.GetType().IsArray && !(value is IEnumerable))
                {
                    // If this content is still serializable, recursion
                    result.Add(info.Name, Serialize(value));
                }
                else
                {
                    // Otherwise, basic type is added to the dictionnary
                    result.Add(info.Name, value);
                }
            }

            return result;
        }
    }
}
