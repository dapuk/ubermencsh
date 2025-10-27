using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OrderService.Services;
using OrderService.Health;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks().AddCheck<SqlHealthCheck>("sql_db");

builder.Services.AddSingleton<IDbService, DbService>();
builder.Services.AddScoped<IOrderService, OrderService.Services.OrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();
app.MapDefaultControllerRoute();
app.MapHealthChecks("/healthz", new HealthCheckOptions { Predicate = _ => true });

app.Run();
