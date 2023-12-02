

using TimeasyCore.src.Core;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.Constraints;
using TimeasyScheduler.src.Models;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public interface IValidator
    {
        ValidationResult Validate(Schedule solution, CreateTimetableConfig timetable);
    }
}
