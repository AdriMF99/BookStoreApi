using BookStoreApi.Models;
using BookStoreApi.Services;
using BookStoreApi.StaticClasses;
using ProjectTracker.SignalR.Interfaces;
using ProjectTracker.SignalR.Services;
using ProjectTracker.SignalR.Services.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddControllers()
    .AddJsonOptions(
    options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
});*/

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ElevatedRights", policy => policy.RequireRole("Administrator", "PowerUser", "BackupAdministrator"));
    options.AddPolicy("Normales", policy => policy.RequireRole("Finance"));
});

builder.Services.AddSingleton<BooksService>();
builder.Services.AddTransient<IRealTimeUpdateInfoService, RealTimeUpdateInfoService>();
builder.Services.AddSingleton<ClientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<RealTimeUpdateHub>("/RealTimeUpdateHub");

app.UseMiddleware<AfterActionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
