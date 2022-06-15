using CrazyClearConfiguration.Core;
using System.Dynamic;
using System.Text.Json;

namespace CrazyClearConfiguration.Adapter.Json;

public class JsonConfigAdaptor : IConfigPort
{
    private readonly string _jsonSource;

    public JsonConfigAdaptor(string jsonSource)
    {
        _jsonSource = jsonSource;
    }

    public Task<ExpandoObject> Read()
    {
        var jsonText = File.ReadAllText(_jsonSource);
        var expandoObject = JsonSerializer.Deserialize<ExpandoObject>(jsonText) ?? new ExpandoObject();

        return Task.FromResult(expandoObject);
    }

    public Task Write(ExpandoObject config)
    {
        throw new NotImplementedException();
    }
}