﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace CPlugin.Net;

/// <summary>
/// Represents a configuration to get the plugin files from a json.
/// </summary>
/// <remarks>
/// The section must be called <c>Plugins</c> and its value must be an array of strings.
/// <para>Example:</para>
/// <c>
/// { "Plugins": [ "MyPlugin1.dll", "MyPlugin2.dll" ] }
/// </c>
/// </remarks>
public class CPluginJsonConfiguration : CPluginConfigurationBase
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="CPluginJsonConfiguration"/> class.
    /// </summary>
    /// <param name="configuration">
    /// A set of key/value application configuration properties.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <c>configuration</c> is <c>null</c>.
    /// </exception>
    public CPluginJsonConfiguration(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        _configuration = configuration;
    }

    /// <inheritdoc />
    public override IEnumerable<string> GetPluginFiles()
    {
        var values = _configuration
            .GetSection("Plugins")
            .Get<string[]>();

        return values is null ? [] : values.Select(GetPluginPath);
    }
}
