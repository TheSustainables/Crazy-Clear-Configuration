using System.IO;
using Xunit;

namespace CrazyClearConfiguration.Adapter.Configuration.Json.Tests
{
    public class JsonConfigAdaptorTests
    {
        private const string JsonSource = "TestSettings.json";
        private const string UpdatedJsonSource = "UpdatedTestSettings.json";

        public JsonConfigAdaptorTests()
        {
            File.Copy(JsonSource, UpdatedJsonSource, overwrite: true);
        }

        [Fact]
        public async void CanReadSimpleValuesFromJson()
        {
            var adaptor = new JsonConfigAdaptor(JsonSource);

            dynamic expandoObject = await adaptor.Read();

            Assert.Equal("Hello 1", expandoObject.String);
            Assert.Equal(1L, expandoObject.Int);
            Assert.Equal(true, expandoObject.Boolean);
            Assert.Equal(new string[] { "Item 1", "Item 2" }, expandoObject.Array);

            Assert.Equal("Hello 2", expandoObject.Object.String);
            Assert.Equal(2L, expandoObject.Object.Int);
            Assert.Equal(true, expandoObject.Object.Boolean);
            Assert.Equal(new string[] { "Item 3", "Item 4" }, expandoObject.Object.Array);

            Assert.Equal("Hello 3", expandoObject.ObjectArray[0].String);
            Assert.Equal(3L, expandoObject.ObjectArray[0].Int);
            Assert.Equal(true, expandoObject.ObjectArray[0].Boolean);

            Assert.Equal("Hello 4", expandoObject.ObjectArray[1].String);
            Assert.Equal(4L, expandoObject.ObjectArray[1].Int);
            Assert.Equal(true, expandoObject.ObjectArray[1].Boolean);
        }

        [Fact]
        public async void CanWriteSimpleValuesToJson()
        {
            var adaptor = new JsonConfigAdaptor(UpdatedJsonSource);

            dynamic expandoObjectBefore = await adaptor.Read();

            expandoObjectBefore.String = "Updated String";
            expandoObjectBefore.NewString = "New String";
            expandoObjectBefore.Object.String = "Updated Object String";
            expandoObjectBefore.Object.NewString = "New Object String";
            expandoObjectBefore.Array[0] = "Updated Array Item";
            expandoObjectBefore.Array.Add("New Array Item");

            await adaptor.Write(expandoObjectBefore);

            dynamic expandoObjectAfter = await adaptor.Read();

            Assert.Equal("Updated String", expandoObjectAfter.String);
            Assert.Equal("New String", expandoObjectAfter.NewString);
            Assert.Equal("Updated Object String", expandoObjectAfter.Object.String);
            Assert.Equal("New Object String", expandoObjectAfter.Object.NewString);
            Assert.Equal("Updated Array Item", expandoObjectAfter.Array[0]);
            Assert.Equal("New Array Item", expandoObjectAfter.Array[2]);
        }
    }
}