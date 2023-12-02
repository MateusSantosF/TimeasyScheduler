
using TimeasyCore.src.Core;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.Constraints;
using TimeasyScheduler.src.Models;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public class SolutionCost
    {
        private List<IValidator> validators = new List<IValidator>();

        public Dictionary<string, int> failedValidationMetrics = new Dictionary<string, int>();

        public int TotalWeight { get; set; } = 0;

        public SolutionCost()
        {
            AddValidator(new RoomCapacityValidator());
            AddValidator(new RoomTypeValidator());
            AddValidator(new DailyGapValidator());
            AddValidator(new InstituteIntervalConflictValidator());
        }

        public void AddValidator(IValidator validator)
        {
            validators.Add(validator);
            failedValidationMetrics.TryAdd(validator.GetType().Name, 0);
        }

        public SolutionCost ValidateAll(Schedule solution, CreateTimetableConfig timetable)
        {
            var result = new ValidationResult();

            foreach (var validator in validators)
            {
                var validationResult = validator.Validate(solution, timetable);
                TotalWeight += validationResult.TotalWeight;
                if(!validationResult.IsValid)
                    failedValidationMetrics[validator.GetType().Name] += validationResult.FailedCount;
            }

            return this;
        }

        public void IncrementMetrics(Dictionary<string, int> otherValidationResult)
        {
            foreach (var key in otherValidationResult.Keys)
            {
                this.failedValidationMetrics[key] += otherValidationResult[key];
            }
        }

    }
}
