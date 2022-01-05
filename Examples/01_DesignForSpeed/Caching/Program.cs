var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
 {
     // Need Redis?  docker run --rm --name redis -p 32768:6379 -d redis redis-server --appendonly yes
     options.Configuration = "localhost:32768,defaultDatabase=5";
     options.InstanceName = "demo:"; // appended to all keys
 });

var app = builder.Build();
app.MapControllers();

app.Run();
