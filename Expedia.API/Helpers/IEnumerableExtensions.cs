using System;
using System.Dynamic;
using System.Reflection;

namespace Expedia.API.Helpers
{
	public static class IEnumerableExtensions
	{
		public static IEnumerable<ExpandoObject> ShapeData<TSource>(
			this IEnumerable<TSource> source,
			string fields
            )
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			var expandoObjectList = new List<ExpandoObject>();
			// reflection
			var propertyInfoList = new List<PropertyInfo>();

			if (string.IsNullOrWhiteSpace(fields))
			{
				var propertyInfos = typeof(TSource)
					.GetProperties(
						BindingFlags.IgnoreCase |
						BindingFlags.Public |
						BindingFlags.Instance
					);
				propertyInfoList.AddRange(propertyInfos);

            } else
			{
				var filedsAfterSplit = fields.Split(",");
				foreach(var filed in filedsAfterSplit)
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
						throw new Exception($"Property {propertyInfo} not found" +
							$" {typeof(TSource)}");
					}

					propertyInfoList.Add(propertyInfo);
                }
			}

			foreach(TSource sourceObj in source)
			{
				var dataShapedObject = new ExpandoObject();

				foreach(var propertyInfo in propertyInfoList)
				{
					var propertyValue = propertyInfo.GetValue(sourceObj);

					((IDictionary<string, object>)dataShapedObject)
						.Add(propertyInfo.Name, propertyValue);
                }

				expandoObjectList.Add(dataShapedObject);
			}

			return expandoObjectList;

        }
	}
}

