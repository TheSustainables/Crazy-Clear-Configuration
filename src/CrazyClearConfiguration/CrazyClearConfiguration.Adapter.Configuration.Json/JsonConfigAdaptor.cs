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

    public async Task<ExpandoObject> Read()
    {
        var jsonContent = await File.ReadAllTextAsync(_jsonSource);
        var expandoObject = JsonConvert.DeserializeObject<ExpandoObject>(jsonContent, new ExpandoObjectConverter());

        return expandoObject ?? new ExpandoObject();
    }

    public async Task Write(ExpandoObject config)
    {
        var jsonContent = JsonConvert.SerializeObject(config, Formatting.Indented);

        await File.WriteAllTextAsync(_jsonSource, jsonContent);
    }
}