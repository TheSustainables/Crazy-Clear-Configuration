using System.Dynamic;

namespace CrazyClearConfiguration.Core.Extensions
{
    public static class ExpandoObjectExtensions
    {
        public static object? GetValue(this ExpandoObject expandoObject, string key)
        {
            return expandoObject.Single(x => x.Key == key).Value;
        }
    }
}
