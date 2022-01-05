using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

public class InMemoryCacheController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;

    public InMemoryCacheController(IMemoryCache memoryCache)
    {
        this._memoryCache = memoryCache;
    }

    [HttpGet("/cache/memory/{key}")]
    public async Task<IActionResult> GetAsync(string key)
    {
        var value = _memoryCache.Get(key);
        if (value == null)
        {
            // long running task, lol - this is a demo
            await Task.Delay(new Random().Next(1000, 5000));

            value = Guid.NewGuid().ToString();
            _memoryCache.Set(key, value);
        }

        return Ok(value);
    }
}
