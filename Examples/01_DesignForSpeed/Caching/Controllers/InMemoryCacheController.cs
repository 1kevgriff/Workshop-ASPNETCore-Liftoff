using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

public class InMemoryCacheController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;

    public InMemoryCacheController(IMemoryCache memoryCache)
    {
        this._memoryCache = memoryCache;
    }

    [HttpGet("/cache/memory")]
    public IActionResult Get()
    {
        var value = _memoryCache.Get("key");
        if (value == null)
        {
            _memoryCache.Set("key", "value");
            value = "value";
        }

        return Ok(value);
    }
}