var builder = WebApplication.CreateBuilder(args);

// =============================================================================
// Service Registration
// =============================================================================

// Configure Controllers
builder.Services.AddControllers();

// Configure Swagger/OpenAPI for development documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS - SafeWatch AI Frontend Access Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("SafeWatchAllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")  // Vite/Vue.js dev server
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// =============================================================================
// HTTP Request Pipeline Configuration
// =============================================================================

// Enable Swagger/OpenAPI only in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS middleware must be placed before Authorization and Controller mapping
app.UseCors("SafeWatchAllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
