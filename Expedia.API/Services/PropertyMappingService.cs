using System;
using System.Reflection;
using Expedia.API.Dtos;
using Expedia.API.Models;

namespace Expedia.API.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _touristRoutePropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"Id", new PropertyMappingValue(new List<string>(){"Id"}) },
                {"Title", new PropertyMappingValue(new List<string>(){"Title"}) },
                {"Rating", new PropertyMappingValue(new List<string>(){"Rating"}) },
                {"OriginalPrice", new PropertyMappingValue(new List<string>(){"OriginalPrice"}) },
            };

        public PropertyMappingService()
        {
            _propertyMappings.Add(
                new PropertyMapping<TouristRouteDto, TouristRoute>(
                    _touristRoutePropertyMapping
                    )
                );
        }

        private IList<IPropertyMapping> _propertyMappings =
            new List<IPropertyMapping>();


        public Dictionary<string, PropertyMappingValue>
            GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping =
                _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()
                    ._mappingDictionary;
            }

            throw new Exception(
                $"Cannot find exact property mapping instance for <{typeof(TSource)}, {typeof(TDestination)}>"
                );
        }

        public bool IsMappingExists<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(",");
            foreach(var filed in fieldsAfterSplit)
            {
                var trimmedField = filed.Trim();
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsPropertiesExists<T>(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(",");
            foreach (var filed in fieldsAfterSplit)
            {
                var propertyName = filed.Trim();
                var propertyInfo = typeof(T).GetProperty(
                    propertyName,
                    BindingFlags.IgnoreCase |
                    BindingFlags.Public |
                    BindingFlags.Instance
                    );
                if (propertyInfo == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

