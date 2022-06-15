using System.Dynamic;
using CrazyClearConfiguration.Core.Ports;

namespace CrazyClearConfiguration.Core.UseCases;

public class UpdateConfigurationUseCase
{
    private readonly IConfigPort _configPort;

    public UpdateConfigurationUseCase(IConfigPort configPort)
    {
        _configPort = configPort;
    }

    public async Task Execute(ExpandoObject updatedConfiguration)
    {
        await _configPort.Write(updatedConfiguration);
    }
}