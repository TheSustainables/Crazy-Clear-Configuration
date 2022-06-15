using CrazyClearConfiguration.Core.Ports;

namespace CrazyClearConfiguration.Core.UseCases;

public class GetConfigurationUseCase
{
    private readonly IConfigPort _configPort;

    public GetConfigurationUseCase(IConfigPort configPort)
    {
        _configPort = configPort;
    }
    
    public async Task<dynamic> Execute()
    {
        return await _configPort.Read();
    }
}