using FrenchAdverbes.AllConstants;

namespace FrenchAdverbes.AllAdverbeRepository;

internal static partial class AdverbeRepository
{
    public static readonly IReadOnlyList<string> T = new[]
    {
        Constants.Totalement,
        Constants.Tres,
        Constants.Tranquillement,
        Constants.Tard,
        Constants.Tant,
    };
}
