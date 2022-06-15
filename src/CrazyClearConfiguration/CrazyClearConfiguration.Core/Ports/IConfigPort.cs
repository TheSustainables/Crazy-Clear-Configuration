using System.Dynamic;

namespace CrazyClearConfiguration.Core.Ports
{
    public interface IConfigPort
    {
        Task<ExpandoObject> Read();
        Task Write(ExpandoObject config);
    }
}
