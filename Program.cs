using Microsoft.EntityFrameworkCore;
using Chirp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ChirpDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
        Title = "Project Chirps API",
        Description = "Project Chirps Backend Development",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Chirps Backend V1");
    });
}

app.MapGet("/getchirps", async (ChirpDb db) => await db.Chirps.ToListAsync());

app.MapPost("/postchirp", async (ChirpDb db, ChirpData chirp) => {
    await db.Chirps.AddAsync(chirp);
    await db.SaveChangesAsync();
    return Results.Created($"/chirp/{chirp.Uuid}", chirp);
});

app.Run();