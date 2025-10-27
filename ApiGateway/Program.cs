using Yarp.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


// CORS Policydock
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

app.MapGet("/", () => Results.Ok(new { ok = true, service = "ApiGateway" }));
app.UseCors("GlobalCORS"); // ?? PENTING: Pindah ke sini
app.MapReverseProxy();

app.Run();
