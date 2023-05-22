using System;
using System.Dynamic;
using System.Reflection;

namespace Expedia.API.Helpers
{
	public static class ObjectExtensions
	{

		public static ExpandoObject ShapeData<TSource>(
			this TSource source,
			string fields
			)
		{
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var dataShapedObject = new ExpandoObject();

            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertyInfos = typeof(TSource)
                    .GetProperties(
                        BindingFlags.IgnoreCase |
                        BindingFlags.Public |
                        BindingFlags.Instance
                    );
                foreach(var propertyInfo in propertyInfos)
                {
                    var propertyValue = propertyInfo.GetValue(source);
                    ((IDictionary<string, object>)dataShapedObject)
                        .Add(propertyInfo.Name, propertyValue);
                }
            } else
            {
                var filedsAfterSplit = fields.Split(",");
                foreach (var filed in filedsAfterSplit)
                {
                    var propertyName = filed.Trim();
                    var propertyInfo = typeof(TSource)
                        .GetProperty(
                            propertyName,
                            BindingFlags.IgnoreCase |
                            BindingFlags.Public |
                            BindingFlags.Instance
                        );
                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property {propertyName} not found" +
                            $" {typeof(TSource)}");
                    }

                    var propertyValue = propertyInfo.GetValue(source);

                    ((IDictionary<string, object>)dataShapedObject)
                            .Add(propertyInfo.Name, propertyValue);
                }
            }
            
                   

            return dataShapedObject;
        }
	}
}

