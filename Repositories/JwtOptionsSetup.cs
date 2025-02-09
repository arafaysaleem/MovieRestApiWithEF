using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MovieRestApiWithEF.Core.Models;

namespace MovieRestApiWithEF.Application;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    ///  When we try to inject our JwtOptions somewhere, the JwtOptionsSetup.Configure method will be called first the calculate the correct values.
    /// </summary>
    /// <param name="options"></param>
    public void Configure(JwtOptions options)
    {
        _configuration
            .GetSection(JwtOptions.SectionName)
            .Bind(options);
    }
}
