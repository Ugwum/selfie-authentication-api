using Microsoft.OpenApi.Models;
using Selfie_Authentication.API.Model;
using Selfie_Authentication.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Add services to the container.

builder.Services.Configure<AWSSettings>(builder.Configuration.GetSection("AWSSettings"));
builder.Services.AddScoped<ISelfieAuthenticationService, SelfieAuthenticationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Selfie Authenticate API Service",
        Version = "v1",
        Description = "Rest API for Selfie Authenticate using Amazon Rekognition and S3 Bucket"
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
