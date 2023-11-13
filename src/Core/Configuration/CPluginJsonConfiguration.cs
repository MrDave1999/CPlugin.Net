using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace CPlugin.Net;

/// <summary>
/// Represents a configuration to get the plugin files from a json.
/// </summary>
/// <remarks>
/// The section must be called <c>Plugins</c> and its value must be an array of strings.
/// <para>
/// Example:
/// <para>{</para>
/// "Plugins": [ "MyPlugin1.dll", "MyPlugin2.dll" ]
/// <para>}</para>
/// </para>
/// </remarks>
public class CPluginJsonConfiguration : CPluginConfigurationBase
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="CPluginJsonConfiguration"/> class.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// <c>configuration</c> is <c>null</c>.
    /// </exception>
    public CPluginJsonConfiguration(IConfiguration configuration)
    {
        if(configuration is null) 
            throw new ArgumentNullException(nameof(configuration));

        _configuration = configuration;
    }

    /// <inheritdoc />
    public override IEnumerable<string> GetPluginFiles()
    {
        var recoveredSection = _configuration.GetSection("Plugins").Get<string[]>();
        if(recoveredSection is null)
        {
            var message =
            """
            'Plugins' section not found in json file.
            Example:
            {
                "Plugins": [ "MyPlugin1.dll", "MyPlugin2.dll" ]
            }
            """;
            throw new InvalidOperationException(message);
        }
        return recoveredSection.Select(GetPluginPath);
    }
}
