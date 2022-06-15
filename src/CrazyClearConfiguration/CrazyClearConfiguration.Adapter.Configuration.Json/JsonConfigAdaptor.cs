using CrazyClearConfiguration.Core;
using System.Dynamic;
using System.Text.Json;

namespace CrazyClearConfiguration.Adapter.Configuration.Json;

public class JsonConfigAdaptor : IConfigPort
{
    private readonly string _jsonSource;

    public JsonConfigAdaptor(string jsonSource)
    {
        _jsonSource = jsonSource;
    }

    public Task<ExpandoObject?> Read()
    {
        var jsonContent = File.ReadAllText(_jsonSource);
        var expandoObject = JsonSerializer.Deserialize<ExpandoObject>(jsonContent) ?? new ExpandoObject();

        return Task.FromResult(expandoObject);
    }

    public async Task Write(ExpandoObject config)
    {
        var jsonContent = JsonSerializer.Serialize(config);

        await File.WriteAllTextAsync(_jsonSource, jsonContent);
    }
}