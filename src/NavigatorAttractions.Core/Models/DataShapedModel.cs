using System.Dynamic;
using System.Reflection;

namespace NavigatorAttractions.Core.Models
{
    public class DataShapedModel<T> // where T : ModelBase
    {
        public dynamic? CreateDataShapedObject(T? objectToShaped, List<string>? lstOfFields)
        {
            if (lstOfFields == null || !lstOfFields.Any())
            {
                return objectToShaped;
            }

            var objectToReturn = new ExpandoObject();
            foreach (var field in lstOfFields)
            {
                var property = typeof(T)
                    .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null)
                {
                    var fieldValue = property.GetValue(objectToShaped, null);

                    (((IDictionary<string, object>)objectToReturn)!).Add(field, fieldValue);
                }
                else
                {
                    (((IDictionary<string, object>)objectToReturn)!).Add(field, string.Empty);
                }
            }

            return objectToReturn;
        }

        public IEnumerable<dynamic?> CreateDataShapedObjects(List<T> objectListToShaped, List<string>? lstOfFields)
        {
            return objectListToShaped.Select(res => CreateDataShapedObject(res, lstOfFields));
        }
    }
}
