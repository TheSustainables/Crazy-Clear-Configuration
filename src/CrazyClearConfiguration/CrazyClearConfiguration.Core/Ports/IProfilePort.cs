namespace CrazyClearConfiguration.Core.Ports
{
    public interface IProfilePort
    {
        Task<ConfigProfile> Load(string name);
        Task Save(ConfigProfile profile);
    }

    public class ConfigProfile
    {
        public ConfigProfile(string name, IEnumerable<IConfigSource> configSources)
        {
            Name = name;
            ConfigSources = configSources;
        }

        public string Name { get; }
        public IEnumerable<IConfigSource> ConfigSources { get; }
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
        public string Name { get; }
        public string FilePath { get; }

        public JsonConfigSource(string name, string filePath)
        {
            FilePath = filePath;
            Name = name;
        }
    }
}
