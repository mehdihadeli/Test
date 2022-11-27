# Testing with WebApplicationFactory<T>

This example demonstrate how to test web components using the `WebApplicationFactory<T>` class.

The `RequestCulture` class library defines a sample middleware that sets the current culture based on values found in query string.

NOTE: ASP.Net supports this behavior by default. This sample is not meant to show how to define HTTP request culture.

The `Test` defines two tests that exercise a test controller to simulate how an HTTP request would behave if the `RequestCultureMiddleware` is used.
