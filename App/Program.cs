using System;
using System.Collections.Generic;
using App.Extensions;
using App.Models;
using App.Serializers;
using Bullseye;
using Microsoft.Extensions.DependencyInjection;
using BasicEmployee = App.Models.BasicSerialization.Employee;
using CustomEmployee = App.Models.CustomSerialization.Employee;
using ThirdPartyEmployee = App.Models.ThirdPartySerialization.Employee;

namespace App
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var targets = new Targets();
            var services = new ServiceCollection();
            services.AddTransient(typeof(ThirdPartyXmlSerializer<>));
            services.AddTransient(typeof(IXmlSerializer<>), typeof(XmlSerializer<>));
            services.AddTransient(typeof(IJsonSerializer<>), typeof(JsonSerializer<>));
            var serviceProvider = services.BuildServiceProvider();

            var basicXmlSerializer = serviceProvider.GetRequiredService<IXmlSerializer<BasicEmployee>>();
            var customXmlSerializer = serviceProvider.GetRequiredService<IXmlSerializer<CustomEmployee>>();
            var thirdPartyXmlSerializer = serviceProvider.GetRequiredService<IXmlSerializer<ThirdPartyEmployee>>();
            var basicJsonSerializer = serviceProvider.GetRequiredService<IJsonSerializer<BasicEmployee>>();
            var customJsonSerializer = serviceProvider.GetRequiredService<IJsonSerializer<CustomEmployee>>();
            var thirdPartyJsonSerializer = serviceProvider.GetRequiredService<IJsonSerializer<ThirdPartyEmployee>>();

            targets.Add(TargetTypes.BasicXmlSerialization, () =>
            {
                var employeeBefore = Factory.CreateEmployeeWithBasicSerialization();
                var xml = basicXmlSerializer.Serialize(employeeBefore);
                employeeBefore.Dump("Basic xml serialization done");
                var employeeAfter = basicXmlSerializer.Deserialize(xml);
                employeeAfter.Dump("Basic xml deserialization done");
            });
            targets.Add(TargetTypes.CustomXmlSerialization, () =>
            {
                var employeeBefore = Factory.CreateEmployeeWithCustomSerialization();
                var xml = customXmlSerializer.Serialize(employeeBefore);
                employeeBefore.Dump("Custom xml serialization done");
                var employeeAfter = customXmlSerializer.Deserialize(xml);
                employeeAfter.Dump("Custom xml deserialization done");
            });
            targets.Add(TargetTypes.ThirdPartyXmlSerialization, () =>
            {
                var employeeBefore = Factory.CreateEmployeeWithThirdPartySerialization();
                var xml = thirdPartyXmlSerializer.Serialize(employeeBefore);
                employeeBefore.Dump("ThirdParty xml serialization done");
                var employeeAfter = thirdPartyXmlSerializer.Deserialize(xml);
                employeeAfter.Dump("ThirdParty xml deserialization done");
            });
            targets.Add(TargetTypes.BasicJsonSerialization, () =>
            {
                var employeeBefore = Factory.CreateEmployeeWithBasicSerialization();
                var json = basicJsonSerializer.Serialize(employeeBefore);
                employeeBefore.Dump("Basic json serialization done");
                var employeeAfter = basicJsonSerializer.Deserialize(json);
                employeeAfter.Dump("Basic json deserialization done");
            });
            targets.Add(TargetTypes.CustomJsonSerialization, () =>
            {
                var employeeBefore = Factory.CreateEmployeeWithCustomSerialization();
                var json = customJsonSerializer.Serialize(employeeBefore);
                employeeBefore.Dump("Custom json serialization done");
                var employeeAfter = customJsonSerializer.Deserialize(json);
                employeeAfter.Dump("Custom json deserialization done");
            });
            targets.Add(TargetTypes.ThirdPartyJsonSerialization, () =>
            {
                var employeeBefore = Factory.CreateEmployeeWithThirdPartySerialization();
                var json = thirdPartyJsonSerializer.Serialize(employeeBefore);
                employeeBefore.Dump("ThirdParty json serialization done");
                var employeeAfter = thirdPartyJsonSerializer.Deserialize(json);
                employeeAfter.Dump("ThirdParty json deserialization done");
            });

            targets.Add(TargetTypes.Default, dependsOn: new List<string>
            {
                TargetTypes.BasicXmlSerialization,
                TargetTypes.CustomXmlSerialization,
                TargetTypes.ThirdPartyXmlSerialization,
                TargetTypes.BasicJsonSerialization,
                TargetTypes.CustomJsonSerialization,
                TargetTypes.ThirdPartyJsonSerialization,
            });

            targets.RunAndExit(args);

            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }

        private static class TargetTypes
        {
            public const string Default = "Default";
            public const string BasicXmlSerialization = "BasicXml";
            public const string CustomXmlSerialization = "CustomXml";
            public const string ThirdPartyXmlSerialization = "ThirdPartyXml";
            public const string BasicJsonSerialization = "BasicJson";
            public const string CustomJsonSerialization = "CustomJson";
            public const string ThirdPartyJsonSerialization = "ThirdPartyJson";
        }
    }
}
