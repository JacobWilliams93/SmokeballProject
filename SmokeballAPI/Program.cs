using System.Net.Http.Headers;
using SmokeballAPI.Factories;
using SmokeballAPI.Interfaces;
using SmokeballAPI.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<ISearchManager, GoogleSearchManager>();
builder.Services.AddScoped<ISearchManager, BingSearchManager>();
builder.Services.AddScoped<ISearchManagerFactory, SearchManagerFactory>();
builder.Services.AddHttpClient("Client", config =>
{
    var productValue = new ProductInfoHeaderValue("SmokeballTest", "1.0");
    config.DefaultRequestHeaders.UserAgent.Add(productValue);
});

var app = builder.Build();
app.UseCors("MyPolicy");
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.MapControllers();
app.Run();
