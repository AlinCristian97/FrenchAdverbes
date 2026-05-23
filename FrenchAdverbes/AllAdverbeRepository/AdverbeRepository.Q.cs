using FrenchAdverbes.AllConstants;

namespace FrenchAdverbes.AllAdverbeRepository;

internal static partial class AdverbeRepository
{
    public static readonly IReadOnlyList<string> Q = new[]
    {
        Constants.Quand,
        Constants.Quotidiennement,
        Constants.Quasiment,
    };
}
