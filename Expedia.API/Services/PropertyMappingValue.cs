using System;
namespace Expedia.API.Services
{
	public class PropertyMappingValue
	{
		public PropertyMappingValue(IEnumerable<string> destinationProperties)
		{
			DestinationProperties = destinationProperties;
        }

        public IEnumerable<string> DestinationProperties { get; private set; }
	}
}

