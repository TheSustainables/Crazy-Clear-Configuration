using CrazyClearConfiguration.Adapter.Configuration.Memory;
using CrazyClearConfiguration.Core.UseCases;
using FluentAssertions;
using Xunit;

namespace CrazyClearConfiguration.Core.Tests.UseCases;

public class GetConfigurationUseCaseTest
{
    [Fact]
    public async Task When_GettingConfiguration_Then_ExpectValidConfiguration()
    {
        //Arrange
        var adapter = new InMemoryConfigAdapter();
        var usecase = new GetConfigurationUseCase(adapter);

        //Act
        var data = await usecase.Execute() as IDictionary<string, object>;

        //Assert
        data.Should().NotBeNull();
        data?["Data"].Should().Be("test1");
    }
}