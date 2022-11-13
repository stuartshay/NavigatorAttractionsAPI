using NavigatorAttractions.Service.Attributes;
using System.Reflection;

namespace NavigatorAttractions.Service.Enums
{
    public class EnumExtensions
    {
        public static T GetPhotoSize<T>(string photoSize)
        {
            var type = typeof(T);
            if (!type.GetTypeInfo().IsEnum)
                throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (field.GetCustomAttribute(typeof(ImageSizeAttribute)) is ImageSizeAttribute attribute)
                {
                    if (attribute.Suffix == photoSize)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == photoSize)
                        return (T)field.GetValue(null);
                }
            }

            return default(T);
        }
    }
}
