using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _service;

    public WeatherController(IWeatherService service)
    {
        _service = service;
    }

    [HttpGet("{city}")]
    public async Task<ActionResult<WeatherDto>> GetWeather(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            return BadRequest("City name is required.");

        try
        {
            var result = await _service.GetWeatherAsync(city);
            return Ok(result);
        }
        catch (Exception ex)
        {
             
            return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
        }
    }
}
