using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using AutoNego.Services;
using AutoNego.Health;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks().AddCheck<SqlHealthCheck>("sql_db");

builder.Services.AddSingleton<IDbService, DbService>();
builder.Services.AddScoped<IAutoNego, AutoNego.Services.AutoNego>();

// CORS Policy
#region Adding a CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("GlobalCORS",
        builder =>
            builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
});
#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();
app.UseCors("GlobalCORS"); // ?? PENTING: Pindah ke sini
app.MapDefaultControllerRoute();
app.MapHealthChecks("/healthz", new HealthCheckOptions { Predicate = _ => true });

app.Run();
