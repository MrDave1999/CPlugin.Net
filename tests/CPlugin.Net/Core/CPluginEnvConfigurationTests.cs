namespace CPlugin.Net.Tests.Core;

public class CPluginEnvConfigurationTests
{
    private class EnvConfigurationTestCases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return "   TestProject.JsonPlugin.dll   TestProject.OldJsonPlugin.dll   TestProject.WebPlugin.dll   ";
            yield return $"{'\t'} TestProject.JsonPlugin.dll{'\t'}TestProject.OldJsonPlugin.dll{'\t'}TestProject.WebPlugin.dll{'\t'}";
            yield return $"{'\n'} TestProject.JsonPlugin.dll{'\n'}TestProject.OldJsonPlugin.dll{'\n'}TestProject.WebPlugin.dll{'\n'}";
            yield return $"{'\r'} TestProject.JsonPlugin.dll{'\r'}TestProject.OldJsonPlugin.dll{'\r'}TestProject.WebPlugin.dll{'\r'}";
            yield return $"{"\r\n"} TestProject.JsonPlugin.dll{"\r\n"}TestProject.OldJsonPlugin.dll{"\r\n"}TestProject.WebPlugin.dll{"\r\n"}";

            yield return
            """
            TestProject.JsonPlugin.dll
            TestProject.OldJsonPlugin.dll
            TestProject.WebPlugin.dll
            """;

            yield return
            """
               TestProject.JsonPlugin.dll
               TestProject.OldJsonPlugin.dll
               TestProject.WebPlugin.dll
            """;

            yield return
            $"""
            {'\t'}{'\t'}   TestProject.JsonPlugin.dll
            {'\t'}{'\t'}   TestProject.OldJsonPlugin.dll
            {'\t'}{'\t'}   TestProject.WebPlugin.dll
            """;
        }
    }

    [TestCaseSource(typeof(EnvConfigurationTestCases))]
    public void GetPluginFiles_WhenPluginFilesArePresent_ShouldReturnsFullPaths(string pluginFiles)
    {
        // Arrange
        Environment.SetEnvironmentVariable("PLUGINS", pluginFiles);
        var envConfiguration = new CPluginEnvConfiguration();
        var basePath = AppContext.BaseDirectory;
        var expectedPaths = new[]
        {
            Path.Combine(basePath, "plugins", "TestProject.JsonPlugin", "TestProject.JsonPlugin.dll"),
            Path.Combine(basePath, "plugins", "TestProject.OldJsonPlugin", "TestProject.OldJsonPlugin.dll"),
            Path.Combine(basePath, "plugins", "TestProject.WebPlugin", "TestProject.WebPlugin.dll")
        };

        // Act
        var actual = envConfiguration.GetPluginFiles().ToList();

        // Assert
        actual.Should().BeEquivalentTo(expectedPaths);
    }

    [Test]
    public void GetPluginFiles_WhenPluginFilesAreNotPresent_ShouldReturnsEmptyEnumerable()
    {
        // Arrange
        Environment.SetEnvironmentVariable("PLUGINS", "    ");
        var envConfiguration = new CPluginEnvConfiguration();

        // Act
        var actual = envConfiguration.GetPluginFiles();

        // Assert
        actual.Should().BeEmpty();
    }

    [Test]
    public void GetPluginFiles_WhenPluginsKeyIsNotPresent_ShouldReturnsEmptyEnumerable()
    {
        // Arrange
        // To ensure that the variable is removed.
        Environment.SetEnvironmentVariable("PLUGINS", default);
        var envConfiguration = new CPluginEnvConfiguration();

        // Act
        var actual = envConfiguration.GetPluginFiles();

        // Assert
        actual.Should().BeEmpty();
    }
}
