using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly IGradeService _gradeService;
    private readonly ILogger<GradeController> _logger;

    public GradeController(IGradeService gradeService, ILogger<GradeController> logger)
    {
        _gradeService = gradeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("GET api/grade called at {Time}", DateTime.UtcNow);

        var grades = await _gradeService.GetAllGradesAsync();
        var gradeList = grades.ToList();

        var totalCount = gradeList.Count;
        var averageValue = gradeList.Count != 0 ? gradeList.Average(g => g.Value) : 0;

        _logger.LogInformation("Returning {Count} grades, average value: {Average}", totalCount, averageValue);

        return Ok(new
        {
            Data = gradeList,
            Statistics = new
            {
                TotalCount = totalCount,
                AverageValue = averageValue,
                RetrievedAt = DateTime.UtcNow
            }
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("GET api/grade/{Id} called at {Time}", id, DateTime.UtcNow);

        if (id <= 0)
        {
            _logger.LogWarning("Invalid id: {Id}", id);
            return BadRequest("Id must be a positive integer.");
        }

        var grade = await _gradeService.GetGradeByIdAsync(id);
        if (grade == null)
        {
            _logger.LogWarning("Grade {Id} not found", id);
            return NotFound($"Grade with Id {id} was not found.");
        }

        return Ok(grade);
    }

    /// <summary>
    /// Returns the first N active passing grades (value >= 5).
    /// </summary>
    [HttpGet("passing")]
    public async Task<IActionResult> GetTopPassing([FromQuery] int count)
    {
        _logger.LogInformation("GET api/grade/passing called with count={Count}", count);

        if (count <= 0)
        {
            return BadRequest("Count must be a positive integer.");
        }

        var grades = await _gradeService.GetTopPassingGradesAsync(count);
        return Ok(grades);
    }
}
