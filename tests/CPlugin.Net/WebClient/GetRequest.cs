namespace CPlugin.Net.Tests.WebClient;

public class GetRequest
{
    [Test]
    public async Task Get_WhenUsersAreObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        var httpResponse = await client.GetAsync("/User");

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    public async Task Get_WhenThereAreNoUsers_ShouldReturnsHttpStatusCodeUnprocessableEntity()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        await client.DeleteAsync("/User");
        var httpResponse = await client.GetAsync("/User");

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
    }

    [Test]
    public async Task Get_WhenEmployeesAreObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        var httpResponse = await client.GetAsync("/Employee");

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    public async Task Get_WhenGettingPluginInfo_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var expected = new[]
        {
            "TestProject.WebPlugin.Startup"
        };

        // Act
        var httpResponse = await client.GetAsync("/Plugin");
        var currentResult = await httpResponse
            .Content
            .ReadFromJsonAsync<string[]>();

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        currentResult.Should().BeEquivalentTo(expected);
    }
}
