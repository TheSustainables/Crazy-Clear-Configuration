using CrazyClearConfiguration.Core.Ports;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace CrazyClearConfiguration.Adapter.Profile.Json.Tests
{
    public class JsonProfileAdaptorTests
    {
        [Fact]
        public async void CanSaveAndLoadProfile()
        {
            var jsonConfigSource = new JsonConfigSource("json", "json_path_1");
            var configSources = new List<IConfigSource>() { jsonConfigSource };
            var newProfile = new ConfigProfile("test_profile", configSources);

            var adaptor = new JsonProfileAdaptor();

            await adaptor.Save(newProfile);

            var loadedProfile = await adaptor.Load("test_profile");

            Assert.Equal(JsonConvert.SerializeObject(newProfile), JsonConvert.SerializeObject(loadedProfile));
        }
    }
}