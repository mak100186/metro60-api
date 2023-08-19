using RestSharp;
using RestSharp.Authenticators;

namespace Metro60.FunctionalTests.Drivers;

[Binding]
internal class Bindings
{
    [AfterTestRun, BeforeTestRun]
    public static void CleanupDatabase()
    {
        Console.WriteLine("Removing products that have Ids:31 and 32");
        var serviceUrl = "https://localhost:7204";

        var client = new RestClient(new RestClientOptions(serviceUrl)
        {
            Authenticator = new HttpBasicAuthenticator("test", "test")
        });

        foreach (var idToDelete in new List<string> { "31", "32" })
        {
            var response = client.Execute(new RestRequest($"product/{idToDelete}")
            {
                RequestFormat = DataFormat.Json,
                Method = Method.Delete
            });

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response);
            }
        }
        
    }
}
