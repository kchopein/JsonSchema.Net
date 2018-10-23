using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonSchemaMigrator.Tests
{
    [TestClass]
    public class MigrationTests
    {
        [TestMethod]
        public void MigratingV1ToV1_ShouldPopulateAllProperty()
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
        public void MigratingV1ToV2_ShouldPopulateRenamedProperty()
        {
            //Arrange:
            var v1 = new V1Dto(5, "String Value");
            var v1Json = JsonStore.Serialize(v1);

            //Act:
            var v2 = JsonStore.Deserialize<V2Dto>(v1Json);

            //Assert:
            v2.IntProperty.Should().Be(v1.IntProperty);
            v2.StringPropertyRenamed.Should().BeEquivalentTo(v1.StringProperty);
            v2.NewStringProp.Should().BeEquivalentTo(V1Dto.NewStringProp);
        }

        [TestMethod]
        public void MigratingV1ToV3_ShouldPopulateAllProperties()
        {
            //Arrange:
            var v1 = new V1Dto(5, "String Value");
            var v1Json = JsonStore.Serialize(v1);

            //Act:
            var v3 = JsonStore.Deserialize<V3Dto>(v1Json);

            //Assert:
            v3.IntProp.Should().Be(v1.IntProperty);
            v3.NewStringProp.Should().BeEquivalentTo(V1Dto.NewStringProp);
        }

        [TestMethod]
        public void MigratingV1ToVXWithNoUpgradePath_ShouldThrowAnException()
        {
            //Arrange:
            var v1 = new V1Dto(5, "String Value");
            var v1Json = JsonStore.Serialize(v1);
            Action action = () => JsonStore.Deserialize<VXDto>(v1Json);

            //Act:
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void WhileMigratingFromV1ToV2_TheCallBackShoulBeInvokedAfterMigration()
        {
            //Arrange:
            var v1 = new V1Dto(5, "String Value");
            var v1Json = JsonStore.Serialize(v1);
            var actionContainer = new ActionContainer();
            JsonStore.RegisterAction<V2Dto, V3Dto>(actionContainer.Callback<V2Dto, V3Dto>);

            //Act:
            var v3 = JsonStore.Deserialize<V3Dto>(v1Json);

            //Assert:
            actionContainer.ExecutedActions[$"{nameof(V2Dto)} - {nameof(V3Dto)}"][1].Should().Be(v3);
        }
    }

    public class ActionContainer
    {
        public Dictionary<string, object[]> ExecutedActions { get; set; } = new Dictionary<string, object[]>();

        public void Callback<Tsource, Ttarget>(Tsource source, Ttarget target)
        {
            this.ExecutedActions.Add($"{nameof(Tsource)} - {nameof(Ttarget)}", new object[] { source, target });
        }
    }
}
