using SupportDeskAI.Application.Interfaces;
using SupportDeskAI.Application.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastucture(builder.Configuration);
builder.Services.AddScoped<ITicketService, TicketService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
