namespace CPlugin.Net.Tests.WebClient;

public class GetPersonsTests
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
}
