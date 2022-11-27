using Microsoft.AspNetCore.Mvc.Testing;

namespace Test;

class WebApplicationFactoryWithHost<TEntryPoint> :
    WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
    private readonly Action<IServiceCollection> configureServices;
    readonly Action<IApplicationBuilder> configure;
    readonly string[] args;

    public Action<IHostBuilder>? HostBuilderCustomization { get; set; }
    public Action<IWebHostBuilder>? WebHostBuilderCustomization { get; set; }

    public WebApplicationFactoryWithHost(Action<IServiceCollection> configureServices, Action<IApplicationBuilder> configure, string[]? args = null)
    {
        this.configureServices = configureServices;
        this.configure = configure;
        this.args = args ?? Array.Empty<string>();
    }

    protected override IHostBuilder CreateHostBuilder()
    {
        var hostBuilder = Host.CreateDefaultBuilder(args);
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