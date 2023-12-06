namespace CPlugin.Net.Tests.WebClient;

public class GetRequest
{
    [Test]
    public async Task Get_WhenUsersAreObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        int expectedUsers = 3;

        // Act
        var httpResponse = await client.GetAsync("/User");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<ListedResult<GetUserResponse>>();

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().HaveCount(expectedUsers);
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
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<ListedResult<GetUserResponse>>();

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        result.IsSuccess.Should().BeFalse();
        result.Data.Should().BeEmpty();
    }

    [Test]
    public async Task Get_WhenEmployeesAreObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        int expectedEmployees = 2;

        // Act
        var httpResponse = await client.GetAsync("/Employee");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<ListedResult<GetEmployeeResponse>>();

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().HaveCount(expectedEmployees);
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
