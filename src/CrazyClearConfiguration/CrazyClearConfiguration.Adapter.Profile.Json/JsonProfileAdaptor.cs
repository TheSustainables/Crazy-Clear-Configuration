using CrazyClearConfiguration.Core.Ports;
using Newtonsoft.Json;

namespace CrazyClearConfiguration.Adapter.Profile.Json
{
    public class JsonProfileAdaptor : IProfilePort
    {
        private const string ProfilesBasePath = "./profiles";
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public JsonProfileAdaptor()
        {
            if (!Directory.Exists(ProfilesBasePath))
            {
                Directory.CreateDirectory(ProfilesBasePath);
            }

            _jsonSerializerSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        private string GetFileName(string name)
        {
            var fileName = name.Trim();

            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalidChar, '_');
            }

            return fileName;
        }

        public async Task<ConfigProfile> Load(string name)
        {
            var fileName = GetFileName(name);
            var filePath = Path.Combine(ProfilesBasePath, fileName);

            if (!File.Exists(filePath))
            {
                return await Task.FromResult(ConfigProfile.Empty);
            }

            var fileContent = await File.ReadAllTextAsync(filePath);
            var profile = JsonConvert.DeserializeObject<ConfigProfile>(fileContent, _jsonSerializerSettings);

            return profile ?? ConfigProfile.Empty;
        }

        public async Task Save(ConfigProfile profile)
        {
            var fileName = GetFileName(profile.Name);
            var filePath = Path.Combine(ProfilesBasePath, fileName);
            var fileContent = JsonConvert.SerializeObject(profile, _jsonSerializerSettings);

            await File.WriteAllTextAsync(filePath, fileContent);
        }
    }
}