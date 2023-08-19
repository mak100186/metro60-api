using Microsoft.Extensions.Configuration;

using RestSharp;
using RestSharp.Authenticators;

namespace Metro60.FunctionalTests.Support;

public class TestBase
{
    protected RestClient Client;
    protected RestClient ClientWithoutAuthentication;
    private readonly ScenarioContext _scenarioContext;

    protected TestBase(ScenarioContext scenarioContext)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        _scenarioContext = scenarioContext;
        var serviceUrl = configuration["serviceUrl"]!;

        Client = new RestClient(new RestClientOptions(serviceUrl)
        {
            Authenticator = new HttpBasicAuthenticator("test", "test")
        });

        ClientWithoutAuthentication = new RestClient(new RestClientOptions(serviceUrl));
    }

    protected RestResponse? Response
    {
        get => _scenarioContext.Get<RestResponse?>(nameof(Response));
        set => _scenarioContext.Set<RestResponse?>(value, nameof(Response));
    }
}
