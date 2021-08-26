using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace App.Serializers
{
    public class XmlSerializer<T> : IXmlSerializer<T>
    {
        private readonly DataContractSerializer _serializer;

        public XmlSerializer()
        {
            var settings = new DataContractSerializerSettings
            {
                KnownTypes = GetKnownTypes()
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
                var obj = (T) _serializer.ReadObject(stream);
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