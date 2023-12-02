
namespace TimeasyScheduler.src.Constraints
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public int FailedCount { get; set; }
        public int TotalWeight { get; set; }

        public ValidationResult()
        {
            IsValid = true;
        }

    }
}
