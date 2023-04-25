using Microsoft.Extensions.Options;
using Proj_Mongo_API.Config;
using Proj_Mongo_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<TurismoMongoSettings>(builder.Configuration.GetSection("TurismoMongoSettings"));
builder.Services.AddSingleton<ITurismoMongoSettings>(s => s.GetRequiredService<IOptions<TurismoMongoSettings>>().Value);
builder.Services.AddSingleton<CitiesService>();
builder.Services.AddSingleton<AddressesService>();
builder.Services.AddSingleton<CustomersService>();

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
