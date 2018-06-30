using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace JsonSchemaMigrator.Tests
{
    [TestClass]
    public class MigrationTests
    {
        [TestMethod]
        public void MigratingV2ToV1_ShouldPopulateAllProperty()
        {
            //Arrange:
            var v1 = new V1Dto(5, "String Value");
            var v1Json = JsonStore.Serialize(v1);

            //Act:
            var v1Copy = JsonStore.Deserialize<V1Dto>(v1Json);

            //Assert:
            v1Copy.IntProperty.Should().Be(v1.IntProperty);
            v1Copy.StringProperty.Should().BeEquivalentTo(v1.StringProperty);
        }

        [TestMethod]
        public void MigratingV2ToV2_ShouldPopulateRenamedProperty()
        {
            //Arrange:
            var v1 = new V1Dto(5, "String Value");
            var v1Json = JsonStore.Serialize(v1);

            //Act:
            var v2 = JsonStore.Deserialize<V2Dto>(v1Json);

            //Assert:
            v2.IntProperty.Should().Be(v1.IntProperty);
            v2.StringPropertyRenamed.Should().BeEquivalentTo(v1.StringProperty);
            v2.NewStringProp.Should().BeEmpty();
        }
    }
}