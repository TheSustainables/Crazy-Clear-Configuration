using System.Dynamic;
using CrazyClearConfiguration.Core.Ports;

namespace CrazyClearConfiguration.Adapter.Configuration.Memory;

public class InMemoryConfigAdapter : IConfigPort
{
    public Task<ExpandoObject> Read()
    {
        dynamic data = new ExpandoObject();
        data.Value = "test1";
        data.Array = new string[]
        {
            "1",
            "2",
            "3",
        };
        dynamic dataObject = new ExpandoObject();
        dataObject.Value = "test2";
        dataObject.Array = new string[]
        {
            "1",
            "2",
            "3",
        };
        data.Object = dataObject;
        return Task.FromResult(data);
    }

    public Task Write(ExpandoObject config)
    {
        return Task.CompletedTask;
    }
}