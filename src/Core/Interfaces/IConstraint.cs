

namespace TimeasyCore.src.Core.Interfaces
{
    public interface IConstraint
    {

        int Id { get; }

        ConstraintType Type { get; }

        bool IsHard { get; }
    }
}
