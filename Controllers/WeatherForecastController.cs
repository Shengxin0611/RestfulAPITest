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
        return await _context.WeatherForecasts.OrderBy(x => x.Date).ThenBy(x => x.TemperatureC).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<WeatherForecast>> Create(WeatherForecast data)
    {
        data.Id = Guid.NewGuid().ToString();
        _context.WeatherForecasts.Add(data);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = data.Id }, data);
    }

    [HttpPut]
    public async Task<IActionResult> Update(WeatherForecast data)
    {        
        var item = await _context.WeatherForecasts.FindAsync(data.Id);

        if (item == null)
        {
            return NotFound();   
        }

        // 更新欄位
        item.Date = data.Date;
        item.TemperatureC = data.TemperatureC;
        item.TemperatureF = data.TemperatureF;
        item.Summary = data.Summary;

        try {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            throw;            
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var item = await _context.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == id);
        
        if (item == null)
        {
            return NotFound();
        } 

        _context.WeatherForecasts.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
