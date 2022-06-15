using System.Dynamic;

namespace CrazyClearConfiguration.Core
{
    public interface IConfigPort
    {
        Task<ExpandoObject> Read();
        Task Write(ExpandoObject config);
    }
}
