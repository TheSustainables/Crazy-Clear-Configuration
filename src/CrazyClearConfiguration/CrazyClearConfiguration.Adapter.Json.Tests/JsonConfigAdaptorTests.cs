using Xunit;

namespace CrazyClearConfiguration.Adapter.Json.Tests
{
    public class JsonConfigAdaptorTests
    {
        [Fact]
        public async void CanReadJson()
        {
            var adaptor = new JsonConfigAdaptor("TestSettings.json");
            var expandoObject = await adaptor.Read();

            foreach (var item in expandoObject)
            {

            }

            //Assert.Equal("Hello 1", expandoObject. .String);
            //Assert.Equal(1, expandoObject["Int"]);
            //Assert.Equal(true, expandoObject["Boolean"]);
        }
    }
}