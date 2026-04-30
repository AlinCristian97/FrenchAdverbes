using FrenchAdverbes.AllConstants;

namespace FrenchAdverbes.AllAdverbeRepository;

internal static partial class AdverbeRepository
{
    public static readonly IReadOnlyList<string> I = new[]
    {
        Constants.Immediatement,
        Constants.Incroyablement,
        Constants.Instinctivement,
        Constants.Intensement,
        Constants.Involontairement,
        Constants.Inutilement,
    };
}
