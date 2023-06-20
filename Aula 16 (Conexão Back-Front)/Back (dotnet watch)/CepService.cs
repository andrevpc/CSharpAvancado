using System.Net;
public class CepService : ICepService
{
    public CepService(string service)
    => this.client = new HttpClient()
    {
        BaseAddress = new Uri(service)
    };
    private HttpClient client;
    public async Task<CepData> Get(string cep)
    {
        var response = await client
        .GetAsync($"/{cep}/json");
        if (response.StatusCode != HttpStatusCode.OK)
            return null;
        var obj = await response.Content.ReadFromJsonAsync<CepData>();
        return obj;
    }
}