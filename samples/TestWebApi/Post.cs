namespace Example.TestWebApi;

public class Post
{
    [TestCase("/Person")]
    [TestCase("/People")]
    public async Task Post_WhenPersonIsCreated_ShouldReturnsHttpStatusCodeCreated(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        var httpResponse = await client.PostAsJsonAsync(requestUri, new {});
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        result.IsSuccess.Should().BeTrue();
    }
}
