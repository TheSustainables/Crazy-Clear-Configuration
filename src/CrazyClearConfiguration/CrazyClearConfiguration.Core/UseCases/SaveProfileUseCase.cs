using CrazyClearConfiguration.Core.Ports;

namespace CrazyClearConfiguration.Core.UseCases;

public class SaveProfileUseCase
{
    private readonly IProfilePort _profilePort;

    public SaveProfileUseCase(IProfilePort profilePort)
    {
        _profilePort = profilePort;
    }

    public async Task Execute(ConfigProfile profile)
    {
        await _profilePort.Save(profile);
    }
}