using CheckQuota.Services;
using CheckQuota.Health;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks().AddCheck<SqlHealthCheck>("mysql_db");

builder.Services.AddSingleton<IDbService, DbService>();
builder.Services.AddScoped<IQuotaService, QuotaService>();

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
