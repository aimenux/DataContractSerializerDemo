using App.Models;
using App.Serializers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicEmployee = App.Models.BasicSerialization.Employee;
using CustomEmployee = App.Models.CustomSerialization.Employee;
using ThirdPartyEmployee = App.Models.ThirdPartySerialization.Employee;

namespace Tests
{
    [TestClass]
    public class XmlSerializerTests
    {
        [TestMethod]
        public void Should_Serialize_Deserialize_Basic_Models_With_Xml()
        {
            // arrange
            var before = Factory.CreateEmployeeWithBasicSerialization();
            var serializer = new XmlSerializer<BasicEmployee>();

            // act
            var xml = serializer.Serialize(before);
            var after = serializer.Deserialize(xml);

            // assert
            xml.Should().NotBeNullOrEmpty();
            xml.Should().StartWith("<").And.EndWith(">");
            after.Should().BeEquivalentTo(before);
        }

        [TestMethod]
        public void Should_Serialize_Deserialize_Custom_Models_With_Xml()
        {
            // arrange
            var before = Factory.CreateEmployeeWithCustomSerialization();
            var serializer = new XmlSerializer<CustomEmployee>();

            // act
            var xml = serializer.Serialize(before);
            var after = serializer.Deserialize(xml);

            // assert
            xml.Should().NotBeNullOrEmpty();
            xml.Should().StartWith("<").And.EndWith(">");
            after.Should().BeEquivalentTo(before);
        }

        [TestMethod]
        public void Should_Serialize_Deserialize_ThirdParty_Models_With_Xml()
        {
            // arrange
            var before = Factory.CreateEmployeeWithThirdPartySerialization();
            var serializer = new XmlSerializer<ThirdPartyEmployee>();

            // act
            var xml = serializer.Serialize(before);
            xml.Should().StartWith("<").And.EndWith(">");
            var after = serializer.Deserialize(xml);

            // assert
            xml.Should().NotBeNullOrEmpty();
            after.Should().BeEquivalentTo(before);
        }
    }
}