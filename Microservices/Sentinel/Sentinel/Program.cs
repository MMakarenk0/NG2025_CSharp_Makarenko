using Sentinel.Middlewares;
using SentinelBusinessLayer.Injections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRefitClients(builder.Configuration);
builder.Services.AddSentinelServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RefitExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
