using Flights.Application;
using Flights.Api.Endpoints;
using Flights.Infrastructure;
using Flights.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddFlightsApplication();

builder.Services.AddFlightsInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials()
);

app.UseSwagger();
app.UseSwaggerUI();

app.UseCustomExceptionHandler();

app.MapFlightEndpoints();

await app.RunAsync();