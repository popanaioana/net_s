using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Services;

public class GradeService : IGradeService
{
    private readonly IGradeReader _gradeReader;

    public GradeService(IGradeReader gradeReader)
    {
        _gradeReader = gradeReader;
    }

    public async Task<IEnumerable<Grade>> GetAllGradesAsync()
    {
        return await _gradeReader.GetAllAsync();
    }

    public async Task<Grade?> GetGradeByIdAsync(int id)
    {
        return await _gradeReader.GetByIdAsync(id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Grade>> GetTopPassingGradesAsync(int count)
    {
        var all = await _gradeReader.GetAllAsync();

        return all
            .Where(g => g.IsActive && g.Value >= 5)
            .Take(count);
    }
}
