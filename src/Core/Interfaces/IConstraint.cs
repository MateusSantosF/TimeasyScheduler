

namespace TimeasyCore.src.Core.Interfaces
{
    public interface IConstraint
    {

        int Id { get; }

        ConstraintType Type { get; }

        // If true, is HardConstraint
        bool Required { get; }



    }
}
