namespace CrazyClearConfiguration.Core.Ports
{
    public interface IProfilePort
    {
        Task<ConfigProfile> Load(string name);
        Task Save(ConfigProfile profile);
    }

    public class ConfigProfile
    {
        public ConfigProfile(string name, IList<IConfigSource> configSources)
        {
            Name = name;
            ConfigSources = configSources;
        }

        public string Name { get; }
        public IList<IConfigSource> ConfigSources { get; }
        public static ConfigProfile Empty => new EmptyConfigProfile();
    }

    public class EmptyConfigProfile : ConfigProfile
    {
        public EmptyConfigProfile() : base(string.Empty, Array.Empty<IConfigSource>()) { }
    }

    public interface IConfigSource
    {
        string Name { get; }
    }

    public class JsonConfigSource : IConfigSource
    {
        public JsonConfigSource(string name, string filePath)
        {
            FilePath = filePath;
            Name = name;
        }

        public string Name { get; }
        public string FilePath { get; }
    }

    public class AzureAppServiceSource : IConfigSource
    {
        public AzureAppServiceSource(string name, string subscription, string resourceGroup, string appServiceName)
        {
            Name = name;
            Subscription = subscription;
            ResourceGroup = resourceGroup;
            AppServiceName = appServiceName;
        }

        public string Name { get; }
        public string Subscription { get; }
        public string ResourceGroup { get; }
        public string AppServiceName { get; }
    }
}
