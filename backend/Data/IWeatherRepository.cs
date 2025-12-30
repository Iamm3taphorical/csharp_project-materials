using Backend.Models;

namespace Backend.Data;

public interface IWeatherRepository
{
    Task<string?> GetCachedWeatherAsync(string city);
    Task CacheWeatherAsync(string city, string json);
    Task LogRequestAsync(string city, bool isHit);
}
