using Microsoft.EntityFrameworkCore;
using NetTopologySuite.IO.Converters;
using WebApplication2.Data;
using WebApplication2.Interface;
using WebApplication2.Models;
using WebApplication2.Repository;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    o => o.UseNetTopologySuite()));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
        options.JsonSerializerOptions.Converters.Add(new GeoJsonConverterFactory());
    });


builder.Services.AddScoped<IPointListService, PointListService>();
builder.Services.AddScoped<DbHelper>();
builder.Services.AddScoped<IEFObjectService, EFObjectService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowReactApp"); // CORS aktif edildi

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
