using FrenchAdverbes.AllConstants;

namespace FrenchAdverbes.AllAdverbeRepository;

internal static partial class AdverbeRepository
{
    public static readonly IReadOnlyList<string> R = new[]
    {
        Constants.Rapidement,
        Constants.Regulierement,
        Constants.Reellement,
        Constants.Remarquablement,
        Constants.Resolument,
        Constants.Recemment,
    };
}
