using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit.Abstractions;

namespace Test;

/// <summary>
/// This WebApplicationFactory only use for testing web components without needing Program entrypoint and it doesn't work for web app with Program file and for this case we should use original WebApplicationFactory.
/// </summary>
/// <typeparam name="TEntryPoint"></typeparam>
class WebApplicationFactoryWithHost<TEntryPoint> :
    WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
    private readonly Action<IServiceCollection> configureServices;
    readonly Action<IApplicationBuilder> configure;
    readonly string[] args;

    public ITestOutputHelper? TestOutputHelper { get; set; }
    public Action<IHostBuilder>? HostBuilderCustomization { get; set; }
    public Action<IWebHostBuilder>? WebHostBuilderCustomization { get; set; }

    public WebApplicationFactoryWithHost(Action<IServiceCollection> configureServices,
        Action<IApplicationBuilder> configure, string[]? args = null)
    {
        this.configureServices = configureServices;
        this.configure = configure;
        this.args = args ?? Array.Empty<string>();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // change existing services ...
            if (TestOutputHelper != null)
                services.AddLogging(b => b.AddXUnit(TestOutputHelper));
        });

        builder.ConfigureTestServices(services =>
        {
            // change existing services ...
        });

        builder.Configure(app =>
        {
            // change application builder
        });
    }

    // This creates a new host, when there is no program file (EntryPoint) for finding the CreateDefaultBuilder - this approach use for testing web components without startup or program
    protected override IHostBuilder CreateHostBuilder()
    {
        var hostBuilder = Host.CreateDefaultBuilder(args);
        // create startup with these configs
        hostBuilder.ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.ConfigureServices(configureServices);
            webBuilder.Configure(configure);

            WebHostBuilderCustomization?.Invoke(webBuilder);
        });

        HostBuilderCustomization?.Invoke(hostBuilder);

        return hostBuilder;
    }
}