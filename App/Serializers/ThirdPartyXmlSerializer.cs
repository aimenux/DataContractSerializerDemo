using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace App.Serializers
{
    public class ThirdPartyXmlSerializer<T> : IXmlSerializer<T>
    {
        private readonly DataContractSerializer _serializer;

        public ThirdPartyXmlSerializer()
        {
            var settings = new DataContractSerializerSettings
            {
                KnownTypes = GetKnownTypes(),
                DataContractSurrogate = new ThirdPartyDataContractSurrogate()
            };

            _serializer = new DataContractSerializer(typeof(T), settings);
        }

        public string Serialize(T obj)
        {
            using (var stream = new MemoryStream())
            {
                _serializer.WriteObject(stream, obj);
                var bytes = stream.ToArray();
                var xml = Encoding.UTF8.GetString(bytes);
                return xml;
            }
        }

        public T Deserialize(string xml)
        {
            var bytes = Encoding.UTF8.GetBytes(xml);
            using (var stream = new MemoryStream(bytes))
            {
                var obj = (T)_serializer.ReadObject(stream);
                return obj;
            }
        }

        private static IEnumerable<Type> GetKnownTypes()
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