using Microsoft.EntityFrameworkCore;
using RestfulAPITest.Models;

namespace RestfulAPITest.DbContexts;

public class WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}