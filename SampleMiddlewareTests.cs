namespace test;

public class SampleMiddlewareTests
{
    [Fact]
    async Task SampleMiddlewareTest()
    {
        var factory = new WebApplicationFactoryWithHost<Dummy>
        (
            configureServices: services =>
            {
                //configure services as needed
            },
            configure: app =>
            {
                app.UseRequestCulture();
                //rest of the required app configuration here
            }
        );

        var client = factory.CreateClient();
        var response = await client.GetAsync("/sample/1?culture=it-IT");

        Assert.True(response.IsSuccessStatusCode);
        //more assertions to validate the culture settings
    }
}