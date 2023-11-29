namespace CPlugin.Net.Tests.WebClient;

public class CreateUserTests
{
    [Test]
    public async Task Post_WhenUserIsCreated_ShouldReturnsHttpStatusCodeCreated()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var request = new 
        { 
            Name = "Dave",
            Password = "1234"
        };

        // Act
        var httpResponse = await client.PostAsJsonAsync("/User", request);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        result.IsSuccess.Should().BeTrue();
    }

    [TestCase(" ", " ")]
    [TestCase("Bob", " ")]
    [TestCase(" ", "Bob")]
    public async Task Post_WhenNameOrPasswordIsEmpty_ShouldReturnsHttpStatusCodeBadRequest(string name, string password)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var request = new
        {
            Name = name,
            Password = password
        };

        // Act
        var httpResponse = await client.PostAsJsonAsync("/User", request);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsSuccess.Should().BeFalse();
    }
}
