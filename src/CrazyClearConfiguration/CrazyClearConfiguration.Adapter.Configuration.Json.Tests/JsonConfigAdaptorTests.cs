using CrazyClearConfiguration.Core.Extensions;
using Xunit;

namespace CrazyClearConfiguration.Adapter.Configuration.Json.Tests
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
                switch (item.Key)
                {
                    case "String":
                        Assert.Equal("Hello 1", item.Value.AsString());
                        break;
                    case "Int":
                        Assert.Equal(1, item.Value.AsInt());
                        break;
                    case "Boolean":
                        Assert.True(item.Value.AsBoolean());
                        break;
                    default:
                        break;
                }
            }
        }
    }
}