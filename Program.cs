using Microsoft.EntityFrameworkCore;
using Chirp.Database;

var builder = WebApplication.CreateBuilder(args);

// Configure a database context using in-memory database
builder.Services.AddDbContext<ChirpDb>(options => options.UseInMemoryDatabase("items"));

// Configures API explorer to discover and describe endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
        Title = "Project Chirps API",
        Description = "Project Chirps Backend Development",
        Version = "v1"
    });
});

// Configure CORS with new policy 'DefaultAppCorsPolicy'
builder.Services.AddCors(options => {
    options.AddPolicy("DefaultAppCorsPolicy", policy => {
        const string frontEndUrl = "http://localhost:4200";

        policy.WithOrigins(frontEndUrl)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

/*  Add CORS middleware with default policy 'DefaultAppCorsPolicy' to allow app send/receive 
    requests from different domains */
app.UseCors("DefaultAppCorsPolicy");

// Add Swagger middleware only if the app is in development mode 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Chirps Backend V1");
    });
}

// All endpoint routing activities shall be organized in the /Controllers folder
Chirp.Controller.ChirpEndpoints.Map(app);

app.Run();