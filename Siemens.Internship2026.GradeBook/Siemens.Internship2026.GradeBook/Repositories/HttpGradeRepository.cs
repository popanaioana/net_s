using System.Net.Http.Json;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class HttpGradeRepository : IGradeReader
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpGradeRepository> _logger;

    public HttpGradeRepository(HttpClient httpClient, ILogger<HttpGradeRepository> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Grade?> GetByIdAsync(int id)
    {
        var all = await GetAllAsync();
        return all.FirstOrDefault(g => g.Id == id);
    }

    public async Task<IEnumerable<Grade>> GetAllAsync()
    {
        _logger.LogInformation("Fetching grades from external endpoint");

        var response = await _httpClient.GetFromJsonAsync<GradeApiResponse>("");

        if (response is null)
        {
            _logger.LogWarning("External endpoint returned null response");
            return Enumerable.Empty<Grade>();
        }

        return response.Items;
    }
}
