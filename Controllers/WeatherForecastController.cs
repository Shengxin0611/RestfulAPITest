using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulAPITest.DbContexts;
using RestfulAPITest.Models;

namespace RestfulAPITest.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(WeatherForecastDbContext context) : ControllerBase
{
    private readonly WeatherForecastDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
    {
        return await _context.WeatherForecasts.ToListAsync();
    }
}
