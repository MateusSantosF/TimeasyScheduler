
using TimeasyCore.src.Core;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.Constraints;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public class ValidationChain
    {
        private List<IValidator> validators = new List<IValidator>();

        public Dictionary<string, int> failedValidationMetrics = new Dictionary<string, int>();

        public int TotalWeight { get; set; }

        public ValidationChain()
        {
            AddValidator(new RoomCapacityValidator());
            AddValidator(new RoomTypeValidator());
            AddValidator(new ScheduleConflictValidator());
            AddValidator(new DailyGapValidator());
            AddValidator(new InstituteIntervalConflictValidator());
        }

        public void AddValidator(IValidator validator)
        {
            validators.Add(validator);
            failedValidationMetrics.TryAdd(validator.GetType().Name, 0);
        }

        public ValidationChain ValidateAll(Schedule solution, Timetable timetable)
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
