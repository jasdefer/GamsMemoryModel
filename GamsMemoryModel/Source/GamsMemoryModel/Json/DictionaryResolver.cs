using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GamsMemoryModel.Json
{
    public class DictionaryResolver<T,U> : DefaultContractResolver
	{
		protected override JsonContract CreateContract(Type objectType)
		{
			if (objectType.GetInterfaces().Any(i => i == typeof(IDictionary<T,U>) ||
				(i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<T,U>))))
			{
				return base.CreateArrayContract(objectType);
			}

			return base.CreateContract(objectType);
		}
	}
}