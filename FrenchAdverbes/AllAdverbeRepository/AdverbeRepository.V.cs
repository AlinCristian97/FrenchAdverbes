using FrenchAdverbes.AllConstants;

namespace FrenchAdverbes.AllAdverbeRepository;

internal static partial class AdverbeRepository
{
    public static readonly IReadOnlyList<string> V = new[]
    {
        Constants.Vraiment,
        Constants.Vite,
        Constants.Vivement,
        Constants.Visiblement,
        Constants.Volontairement,
        Constants.Veritablement,
        Constants.Vaillamment,
    };
}
