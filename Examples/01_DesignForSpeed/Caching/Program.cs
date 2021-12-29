var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();
app.MapControllers();

app.Run();
