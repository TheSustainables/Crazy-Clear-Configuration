using CrazyClearConfiguration.Core.Ports;
using Newtonsoft.Json;

namespace CrazyClearConfiguration.Adapter.Profile.Json
{
    public class JsonProfileAdaptor : IProfilePort
    {
        private const string ProfilesBasePath = "./profiles";

        private string GetFileName(string name)
        {
            var fileName = name.Trim();

            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalidChar, '_');
            }

            return fileName;
        }

        public async Task<ConfigProfile> Get(string name)
        {
            var fileName = GetFileName(name);
            var filePath = Path.Combine(ProfilesBasePath, fileName);

            if (!File.Exists(filePath))
            {
                return await Task.FromResult(ConfigProfile.Empty);
            }

            var fileContent = await File.ReadAllTextAsync(filePath);
            var profile = JsonConvert.DeserializeObject<ConfigProfile>(fileContent);

            return profile ?? ConfigProfile.Empty;
        }

        public async Task Save(string name, ConfigProfile profile)
        {
            var fileName = GetFileName(name);
            var filePath = Path.Combine(ProfilesBasePath, fileName);
            var fileContent = JsonConvert.SerializeObject(profile, Formatting.Indented);

            await File.WriteAllTextAsync(filePath, fileContent);
        }
    }
}