using MongoDB.Entities;

namespace SearchService.Services;

public class AuctionSvcHttpClient 
{
    public readonly HttpClient _httpClient;
    public readonly IConfiguration _config;
    public AuctionSvcHttpClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<List<Item>> GetItemsForSearchDb() 
    {
        var lastUpdated = await DB.Find<Item, string>()
                .Sort(x => x.Descending(a => a.UpdatedAt))
                .Project(x => x.UpdatedAt.ToString())
                .ExecuteFirstAsync();       


        return await  _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"] 
        + "/api/auctions?date=" + lastUpdated);
    }
}