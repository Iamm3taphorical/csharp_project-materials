using Backend.Models;

namespace Backend.Services;

public interface IWeatherService
{
    Task<WeatherDto> GetWeatherAsync(string city);
}
