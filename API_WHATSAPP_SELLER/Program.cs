using API_WHATSAPP_SELLER.Interfaces;
using API_WHATSAPP_SELLER.Models;
using API_WHATSAPP_SELLER.Repository;
using API_WHATSAPP_SELLER.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IConversaRepository, ConversaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<ITwilioService, TwilioService>();
builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));

builder.Services.AddScoped<IBotService, BotService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
