using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

public class DistributedCacheController : ControllerBase
{
    private readonly IDistributedCache _distributedCache;

    public DistributedCacheController(IDistributedCache distributedCache)
    {
        this._distributedCache = distributedCache;
    }

    [HttpGet("/cache/distributed/{key}")]
    public async Task<IActionResult> Get(string key)
    {
        var value = await _distributedCache.GetStringAsync(key);
        if (value == null)
        {
            // long running task, lol - this is a demo
            await Task.Delay(new Random().Next(1000, 5000));

            value = Guid.NewGuid().ToString();
            await _distributedCache.SetStringAsync(key, value);
        }

        return Ok(value);
    }
}