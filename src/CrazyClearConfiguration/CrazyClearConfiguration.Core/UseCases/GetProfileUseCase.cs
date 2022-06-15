using CrazyClearConfiguration.Core.Ports;

namespace CrazyClearConfiguration.Core.UseCases;

public class GetProfileUseCase
{
    private readonly IProfilePort _profilePort;

    public GetProfileUseCase(IProfilePort profilePort)
    {
        _profilePort = profilePort;
    }

    public async Task<ConfigProfile> Execute(string name)
    {
        return await _profilePort.Load(name);
    }
}