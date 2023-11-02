
using TimeasyCore.src.Core;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.Constraints;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public class ValidationChain
    {
        private List<IValidator> validators = new List<IValidator>();

        public void AddValidator(IValidator validator)
        {
            validators.Add(validator);
        }

        public ValidationResult ValidateAll(Schedule solution, Timetable timetable)
        {
            var result = new ValidationResult();

            foreach (var validator in validators)
            {
                var validation = validator.Validate(solution, timetable);
                result.AddValidation(validation);
            }

            return result;
        }

    }
}
