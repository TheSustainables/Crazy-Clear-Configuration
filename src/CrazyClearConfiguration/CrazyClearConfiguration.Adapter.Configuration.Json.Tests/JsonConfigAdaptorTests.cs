using CrazyClearConfiguration.Core.Extensions;
using Xunit;

namespace CrazyClearConfiguration.Adapter.Configuration.Json.Tests
{
    public class JsonConfigAdaptorTests
    {
        [Fact]
        public async void CanReadSimpleValuesFromJson()
        {
            var adaptor = new JsonConfigAdaptor("TestSettings.json");
            var expandoObject = await adaptor.Read();

            Assert.Equal("Hello 1", expandoObject.GetValue("String").AsString());
            Assert.Equal(1, expandoObject.GetValue("Int").AsInt());
            Assert.True(expandoObject.GetValue("Boolean").AsBoolean());
        }

        [Fact]
        public async void CanWriteSimpleValuesToJson()
        {
            var adaptor = new JsonConfigAdaptor("TestSettings.json");

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