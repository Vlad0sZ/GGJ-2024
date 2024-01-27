using BackendGGJ.Behaviours;
using BackendGGJ.Database;
using BackendGGJ.Hubs;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SessionManager>();
builder.Services.AddScoped<ActionDatabase>();
builder.Services.AddSignalR();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});


// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<UserHub>("/hub");
app.MapHub<ClientHub>("/game");

app.UseCors(x => x.AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

// KeepAlive ?
// app.UseWebSockets();
app.Run();