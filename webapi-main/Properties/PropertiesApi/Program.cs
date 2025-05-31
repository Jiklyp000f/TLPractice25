using AutoMapper;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using PropertiesApi.Mapping;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddControllers();
//ошибка тут
// Database
builder.Services.AddDbContext<AppDbContext>( options =>
    options.UseSqlServer( builder.Configuration.GetConnectionString( "Default" ) ) );

// Repositories
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();

// AutoMapper
builder.Services.AddAutoMapper( typeof( PropertyProfile ) );

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//ошибка тут
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
