using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IGradeService
{
    Task<IEnumerable<Grade>> GetAllGradesAsync();
    Task<Grade?> GetGradeByIdAsync(int id);

    /// <summary>
    /// Returns the first <paramref name="count"/> grades that are active
    /// and have a passing value (>= 5).
    /// </summary>
    Task<IEnumerable<Grade>> GetTopPassingGradesAsync(int count);
}
