using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Redis.StackExchange;

var builder = WebApplication.CreateBuilder(args);

// appsettings.json kullanımı için eklediğimiz kod
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Hangfire builder
IConfigurationSection redisSettings = configuration.GetSection("RedisSetting");
string redisConnectionString = $"{redisSettings["RedisEndPoint"]}:{redisSettings["RedisPort"]},password={redisSettings["RedisPassword"]},defaultDatabase={redisSettings["RedisDB"]}";
builder.Services.AddHangfire(x => x.UseRedisStorage(redisConnectionString));
builder.Services.AddHangfireServer();
#endregion

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}

#region Hangfire app
app.UseHangfireDashboard("/HangFireDashBoard", new DashboardOptions
{
    DashboardTitle = "Hangfire DashBoard",
    AppPath = "/Home/HangfireAbout",
    Authorization = new[] { new LocalRequestsOnlyAuthorizationFilter() }
});
app.UseHangfireServer(new BackgroundJobServerOptions
{
    SchedulePollingInterval = TimeSpan.FromSeconds(30),
    WorkerCount = Environment.ProcessorCount * 5
});
GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 3 });
#endregion

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
