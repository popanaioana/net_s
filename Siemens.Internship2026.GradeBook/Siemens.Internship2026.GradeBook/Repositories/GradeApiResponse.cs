using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

internal sealed class GradeApiResponse
{
    public List<Grade> Items { get; set; } = new();
}
