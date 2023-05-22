using System;
using Expedia.API.Services;
using System.Linq.Dynamic.Core;

namespace Expedia.API.Helpers
{
	public static class IQueryableExtensions
	{

		public static IQueryable<T> ApplySort<T>(
			this IQueryable<T> source,
			string orderBy,
			Dictionary<string, PropertyMappingValue> mappingDictionary
			)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
            if (mappingDictionary == null)
            {
                throw new ArgumentNullException("mappingDictionary");
            }
			if (string.IsNullOrEmpty(orderBy))
			{
				return source;
			}

			var orderByString = string.Empty;
			var orderByAfterSplit = orderBy.Split(",");

			// e.g., originalPrice desc, title asc

			foreach(var order in orderByAfterSplit)
			{
				var trimmedOrder = order.Trim();
				var orderDescending = trimmedOrder.EndsWith(" desc");

				var indexOfFirstSpace = trimmedOrder.IndexOf(" ");
				var propetyName = indexOfFirstSpace == -1 ?
					trimmedOrder : trimmedOrder.Remove(indexOfFirstSpace);

				if (!mappingDictionary.ContainsKey(propetyName))
				{
                    throw new ArgumentNullException($"Key mapping for {propetyName} is missing");
                }

				var propertyMappingValue = mappingDictionary[propetyName];
				if (propertyMappingValue == null)
				{
                    throw new ArgumentNullException("propertyMappingValue");
                }

				foreach(var destinationProperty in
					propertyMappingValue.DestinationProperties.Reverse())
				{
					orderByString = orderByString +
						(string.IsNullOrWhiteSpace(orderByString) ? string.Empty : ", ")
						+ destinationProperty
						+ (orderDescending ? " descending" : " ascending");
                }
            }

			return source.OrderBy(orderByString);
        }
    }
}

