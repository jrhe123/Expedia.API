﻿namespace Expedia.API.Services
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();

        bool IsMappingExists<TSource, TDestination>(string fields);
    }
}