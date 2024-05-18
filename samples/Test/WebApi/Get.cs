namespace Example.Test.WebApi;

public class Get
{
    [Test]
    public async Task Get_WhenAppointmentsAreObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        int expectedAppointments = 3;

        // Act
        var httpResponse = await client.GetAsync("/Appointment");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<ListedResult<GetAppointmentResponse>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().HaveCount(expectedAppointments);
    }

    [Test]
    public async Task Get_WhenAppointmentIsObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var requestUri = "/Appointment/5dace456-13d9-42c5-92ea-89869ac44b5c";

        // Act
        var httpResponse = await client.GetAsync(requestUri);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result<GetAppointmentResponse>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
    }

    [Test]
    public async Task Get_WhenAppointmentIsNotFound_ShouldReturnsHttpStatusCodeNotFound()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var requestUri = "/Appointment/5dace456";

        // Act
        var httpResponse = await client.GetAsync(requestUri);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result<GetAppointmentResponse>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.IsSuccess.Should().BeFalse();
        result.Data.Should().BeNull();
    }

    [TestCase("/People")]
    [TestCase("/Person")]
    public async Task Get_WhenPersonsAreObtained_ShouldReturnsHttpStatusCodeOk(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        int expectedPersons = 3;

        // Act
        var httpResponse = await client.GetAsync(requestUri);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result<IEnumerable<GetPersonResponse>>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().HaveCount(expectedPersons);
    }

    [Test]
    public async Task Get_WhenWeatherForecastAreObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        int expectedWeatherForecast = 5;

        // Act
        var httpResponse = await client.GetAsync("/WeatherForecast");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<ListedResult<WeatherForecast>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().HaveCount(expectedWeatherForecast);
    }

    [Test]
    public async Task Get_WhenServiceNameIsObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var expectedServiceName = "TestService";

        // Act
        var httpResponse = await client.GetAsync("/Service");
        var result = await httpResponse
            .Content
            .ReadAsStringAsync();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().Be(expectedServiceName);
    }

    [Test]
    public async Task Get_WhenSummariesAreObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        int expectedSummaries = 10;

        // Act
        var httpResponse = await client.GetAsync("/Summary");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<string[]>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().HaveCount(expectedSummaries);
    }
}
