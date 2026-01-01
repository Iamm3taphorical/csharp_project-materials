using Backend.Data;
using Backend.Models;
using System.Text.Json;

namespace Backend.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository _repository;
    private readonly IConfiguration _config;
     
     
     
    private readonly HttpClient _http;

    public WeatherService(IWeatherRepository repository, IConfiguration config)
    {
        _repository = repository;
        _config = config;
        _http = new HttpClient();  
    }

    public async Task<WeatherDto> GetWeatherAsync(string city)
    {
         
        string? cachedJson = await _repository.GetCachedWeatherAsync(city);

        if (!string.IsNullOrEmpty(cachedJson))
        {
            await _repository.LogRequestAsync(city, true);  
             
            return MapJsonToDto(city, cachedJson, "Cached");
        }

         
        await _repository.LogRequestAsync(city, false);  

         
         
         
         
        
         
         
        string newJson = SimulateApiCall(city);

         
        await _repository.CacheWeatherAsync(city, newJson);

        return MapJsonToDto(city, newJson, "Live API");
    }

    private WeatherDto MapJsonToDto(string city, string json, string source)
    {
         
        var data = JsonSerializer.Deserialize<JsonElement>(json);
        double temp = 0;
        try { temp = data.GetProperty("main").GetProperty("temp").GetDouble(); } catch {}

        return new WeatherDto
        {
            City = city,
            TempC = temp,
            Condition = source,  
            RawJson = json
        };
    }

    private string SimulateApiCall(string city)
    {
         
        var rnd = new Random();
        double temp = rnd.Next(-5, 35);
        string[] conditions = new[] { "Sunny", "Rainy", "Cloudy", "Snowy" };
        string condition = conditions[rnd.Next(conditions.Length)];

        var obj = new
        {
            weather = new[] { new { main = condition, description = condition } },
            main = new { temp = temp, humidity = rnd.Next(30, 90) },
            name = city
        };
        return JsonSerializer.Serialize(obj);
    }
}
