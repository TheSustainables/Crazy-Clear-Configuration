using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.AppService;
using CrazyClearConfiguration.Core.Ports;
using System.Dynamic;

namespace CrazyClearConfiguration.Adapter.Configuration.Azure
{
    public class AzureAppServiceConfigAdaptor : IConfigPort
    {
        private readonly string _subscription;
        private readonly string _resourceGroup;
        private readonly string _appServiceName;

        public AzureAppServiceConfigAdaptor(string subscription, string resourceGroup, string appServiceName)
        {
            _subscription = subscription;
            _resourceGroup = resourceGroup;
            _appServiceName = appServiceName;
        }

        public async Task<ExpandoObject> Read()
        {
            var armClient = new ArmClient(new DefaultAzureCredential(), _subscription);
            var subscription = await armClient.GetDefaultSubscriptionAsync();
            var resourceGroups = subscription.GetResourceGroups();
            var resourceGroup = await resourceGroups.GetAsync(_resourceGroup);
            var resources = resourceGroup.Value.GetGenericResources();
            var appService = resources.Where(x => x.Data.ResourceType == "Microsoft.Web/sites" && x.Data.Name == _appServiceName).First();
            var webSite = armClient.GetWebSiteResource(appService.Id);
            var appSettings = await webSite.GetApplicationSettingsAsync();
            var appSettingsDictionary = appSettings.Value;
            
            IDictionary<string, object?> expandoObject = new ExpandoObject();

            foreach (var item in appSettingsDictionary.Properties)
            {
                if (item.Key.Count(x => x == '[') > 0)
                {
                    // TODO: deal with arrays
                    continue;
                }

                if (item.Key.Count(x => x == ':') == 0)
                {
                    expandoObject[item.Key] = item.Value;
                }
                else if (item.Key.Count(x => x == ':') == 1)
                {
                    var keyParts = item.Key.Split(":");
                    IDictionary<string, object?> secondExpandoObject = new ExpandoObject();

                    if (expandoObject.ContainsKey(keyParts[0]))
                    {
                        secondExpandoObject = expandoObject[keyParts[0]] as IDictionary<string, object?> ?? new ExpandoObject();
                    }

                    expandoObject[keyParts[0]] = secondExpandoObject;
                    secondExpandoObject[keyParts[1]] = item.Value;
                }
                else
                {
                    // TODO: make it recursive to deal with deeper objects
                }
            }

            return (ExpandoObject)expandoObject;
        }

        public Task Write(ExpandoObject config)
        {
            throw new NotImplementedException();
        }
    }
}