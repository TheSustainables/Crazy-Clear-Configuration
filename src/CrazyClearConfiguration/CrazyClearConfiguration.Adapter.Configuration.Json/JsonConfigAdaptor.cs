using CrazyClearConfiguration.Core.Ports;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;

namespace CrazyClearConfiguration.Adapter.Configuration.Json;

public class JsonConfigAdaptor : IConfigPort
{
    private readonly string _jsonSource;

    public JsonConfigAdaptor(string jsonSource)
    {
        _jsonSource = jsonSource;
    }

    public Task<ExpandoObject> Read()
    {
        var jsonContent = File.ReadAllText(_jsonSource);
        var expandoObject = JsonConvert.DeserializeObject<ExpandoObject>(jsonContent, new ExpandoObjectConverter());

        return Task.FromResult(expandoObject ?? new ExpandoObject());
    }

    public async Task Write(ExpandoObject config)
    {
        var jsonContent = JsonConvert.SerializeObject(config);

        await File.WriteAllTextAsync(_jsonSource, jsonContent);
    }
}