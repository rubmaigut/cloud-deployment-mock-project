using Azure.Storage.Blobs;
using AzureFullstackPractice.Data;
using AzureFullstackPractice.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PersonDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING") ??
                         throw new InvalidOperationException("Connection string ‘AzureConn’ not found.")));
builder.Services.AddControllers();
builder.Services.AddCors();

string connectionString = builder.Configuration.GetConnectionString("personfullstackblob");
builder.Services.AddSingleton<BlobServiceClient>(x => new BlobServiceClient(connectionString));
builder.Services.AddScoped<BlobStorageService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.OperationFilter<FormFileOperationFilter>();

});

var app = builder.Build();

app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
app.Use(async (context, next) =>
{
    await next.Invoke();
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();