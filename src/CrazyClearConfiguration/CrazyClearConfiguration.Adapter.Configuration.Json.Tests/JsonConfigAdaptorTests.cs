using CrazyClearConfiguration.Core.Extensions;
using System.IO;
using Xunit;

namespace CrazyClearConfiguration.Adapter.Configuration.Json.Tests
{
    public class JsonConfigAdaptorTests
    {
        private const string JsonSource = "TestSettings.json";
        private const string UpdatedJsonSource = "UpdatedTestSettings.json";

        [Fact]
        public async void CanReadSimpleValuesFromJson()
        {
            var adaptor = new JsonConfigAdaptor(JsonSource);
            var expandoObject = await adaptor.Read();

            Assert.Equal("Hello 1", expandoObject.GetValue("String").AsString());
            Assert.Equal(1, expandoObject.GetValue("Int").AsInt());
            Assert.True(expandoObject.GetValue("Boolean").AsBoolean());
        }

        [Fact]
        public async void CanWriteSimpleValuesToJson()
        {
            File.Copy(JsonSource, UpdatedJsonSource, overwrite: true);

            var adaptor = new JsonConfigAdaptor(UpdatedJsonSource);

            dynamic expandoObjectBefore = await adaptor.Read();

            expandoObjectBefore.String = "Update String";
            expandoObjectBefore.NewString = "New String";

            await adaptor.Write(expandoObjectBefore);

            var expandoObjectAfter = await adaptor.Read();

            Assert.Equal("Update String", expandoObjectAfter.GetValue("String").AsString());
            Assert.Equal("New String", expandoObjectAfter.GetValue("NewString").AsString());
        }
    }
}