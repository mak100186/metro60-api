using RestSharp;
using RestSharp.Authenticators;

namespace Metro60.FunctionalTests.Support;

public class TestBase
{
    protected RestClient Client;
    protected RestClient ClientWithoutAuthentication;
    private readonly ScenarioContext ScenarioContext;

    protected TestBase(ScenarioContext scenarioContext)
    {
        ScenarioContext = scenarioContext;
        var serviceUrl = "https://localhost:7204";

        Client = new RestClient(new RestClientOptions(serviceUrl)
        {
            Authenticator = new HttpBasicAuthenticator("test", "test")
        });

        ClientWithoutAuthentication = new RestClient(new RestClientOptions(serviceUrl));
    }

    protected RestResponse? Response
    {
        get => ScenarioContext.Get<RestResponse?>(nameof(Response));
        set => ScenarioContext.Set<RestResponse?>(value, nameof(Response));
    }
}
