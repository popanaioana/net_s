using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Repositories;
using Siemens.Internship2026.GradeBook.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register the typed HTTP client for the external grades endpoint
builder.Services.AddHttpClient<IGradeReader, HttpGradeRepository>(client =>
{
    client.BaseAddress = new Uri("https://gist.githubusercontent.com/ArdeleanTudor/8ea407832cd9794960e0e6bbd1319f6e/raw/");
});

builder.Services.AddScoped<IGradeService, GradeService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();