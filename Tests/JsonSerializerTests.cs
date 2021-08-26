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
    public class JsonSerializerTests
    {
        [TestMethod]
        public void Should_Serialize_Deserialize_Basic_Models_With_Json()
        {
            // arrange
            var before = Factory.CreateEmployeeWithBasicSerialization();
            var serializer = new JsonSerializer<BasicEmployee>();

            // act
            var json = serializer.Serialize(before);
            var after = serializer.Deserialize(json);

            // assert
            json.Should().NotBeNullOrEmpty();
            json.Should().StartWith("{").And.EndWith("}");
            after.Should().BeEquivalentTo(before);
        }

        [TestMethod]
        public void Should_Serialize_Deserialize_Custom_Models_With_Json()
        {
            // arrange
            var before = Factory.CreateEmployeeWithCustomSerialization();
            var serializer = new JsonSerializer<CustomEmployee>();

            // act
            var json = serializer.Serialize(before);
            var after = serializer.Deserialize(json);

            // assert
            json.Should().NotBeNullOrEmpty();
            json.Should().StartWith("{").And.EndWith("}");
            after.Should().BeEquivalentTo(before);
        }

        [TestMethod]
        public void Should_Serialize_Deserialize_ThirdParty_Models_With_Json()
        {
            // arrange
            var before = Factory.CreateEmployeeWithThirdPartySerialization();
            var serializer = new JsonSerializer<ThirdPartyEmployee>();

            // act
            var json = serializer.Serialize(before);
            var after = serializer.Deserialize(json);

            // assert
            json.Should().NotBeNullOrEmpty();
            json.Should().StartWith("{").And.EndWith("}");
            after.Should().BeEquivalentTo(before);
        }
    }
}
