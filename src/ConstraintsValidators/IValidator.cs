

using TimeasyCore.src.Core;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.Constraints;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public interface IValidator
    {
        ValidationResult Validate(Schedule solution, Timetable timetable);
    }
}
