using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace App.Serializers
{
    public class ThirdPartyJsonSerializer<T> : IJsonSerializer<T>
    {
        private readonly DataContractJsonSerializer _serializer;

        public ThirdPartyJsonSerializer()
        {
            var settings = new DataContractJsonSerializerSettings
            {
                KnownTypes = GetKnownTypes(),
                DataContractSurrogate = new ThirdPartyDataContractSurrogate()
            };

            _serializer = new DataContractJsonSerializer(typeof(T), settings);
        }

        public string Serialize(T obj)
        {
            using (var stream = new MemoryStream())
            {
                _serializer.WriteObject(stream, obj);
                var bytes = stream.ToArray();
                var json = Encoding.UTF8.GetString(bytes);
                return json;
            }
        }

        public T Deserialize(string json)
        {
            var bytes = Encoding.UTF8.GetBytes(json);
            using (var stream = new MemoryStream(bytes))
            {
                var obj = (T) _serializer.ReadObject(stream);
                return obj;
            }
        }

        protected static IEnumerable<Type> GetKnownTypes()
        {
            var currentTypes = typeof(T)
                .Assembly
                .GetTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsSerializable);

            var propertyTypes = typeof(T)
                .GetProperties()
                .Select(p => p.PropertyType)
                .Where(t => t.IsClass)
                .Where(t => t.IsSerializable)
                .Where(t => currentTypes.Contains(t));

            var knownTypes = new List<Type>(propertyTypes)
            {
                typeof(T)
            };

            return knownTypes;
        }
    }
}