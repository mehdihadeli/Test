using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Test
{
    namespace Controllers
    {
        [Route("/api/Sample")]
        public class SampleController : Controller
        {
            [HttpGet]
            public string Index()
            {
                return CultureInfo.CurrentCulture.Name;
            }
        }
    }

    public class SampleMiddlewareTests
    {
        [Fact]
        async Task Setting_culture_to_Italian_returns_expected_result()
        {
            var factory = new WebApplicationFactoryWithHost<Dummy>
            (
                configureServices: services =>
                {
                    //configure services as needed
                    services.AddRouting();
                    services.AddControllers();
                },
                configure: app =>
                {
                    app.UseRequestCulture();
                    app.UseRouting();
                    app.UseEndpoints(builder =>
                    {
                        builder.MapControllers();
                    });
                }
            );

            var client = factory.CreateClient();
            var response = await client.GetAsync("/api/sample?culture=en-US");

            Assert.True(response.IsSuccessStatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("en-US", content);
        }

        [Fact]
        async Task Setting_no_culture_returns_default_EN_result()
        {
            var factory = new WebApplicationFactoryWithHost<Dummy>
            (
                configureServices: services =>
                {
                    //configure services as needed
                    services.AddRouting();
                    services.AddControllers();
                },
                configure: app =>
                {
                    app.UseRequestCulture();
                    app.UseRouting();
                    app.UseEndpoints(builder =>
                    {
                        builder.MapControllers();
                    });
                }
            );

            var client = factory.CreateClient();
            var response = await client.GetAsync("/api/sample?culture=it-IT");

            Assert.True(response.IsSuccessStatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("it-IT", content);
        }
    }
}