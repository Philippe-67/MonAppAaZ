using MonApi.Settings;
using MonApi.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// MongoDB
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));
// Enregistrer le repository et le service pour l'injection de dépendances
builder.Services.AddSingleton<IPrenomsRepository, PrenomsRepository>();
builder.Services.AddSingleton<IPrenomsService, PrenomsService>();

// CORS pour React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5174", policy =>  // 📌 Nom défini 
    {
        policy.WithOrigins("http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// ⚠️ Le nom ici doit être IDENTIQUE à celui défini au-dessus !
app.UseCors("AllowLocalhost5174");  // 📌 Corrigé !
app.UseAuthorization();
app.MapControllers();
app.Run();
