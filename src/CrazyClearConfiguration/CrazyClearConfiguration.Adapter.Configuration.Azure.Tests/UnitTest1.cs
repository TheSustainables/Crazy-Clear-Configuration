using Xunit;

namespace CrazyClearConfiguration.Adapter.Configuration.Azure.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var adaptor = new AzureAppServiceConfigAdaptor("fa31c977-4db7-4493-ba69-7750e263be7a", "ts-general-test", "ts-adviceservice-test");

            await adaptor.Read();
        }
    }
}