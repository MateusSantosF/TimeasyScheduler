namespace TimeasyScheduler.src.Constraints
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public int TotalWeight { get; set; }

        public ValidationResult()
        {
            IsValid = true;
        }

        public void AddValidation(ValidationResult validation)
        {
            if (!validation.IsValid)
            {
                IsValid = false;
            }

            TotalWeight += validation.TotalWeight;

        }

    }
}
