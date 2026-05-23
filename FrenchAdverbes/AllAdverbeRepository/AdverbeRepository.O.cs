using FrenchAdverbes.AllConstants;

namespace FrenchAdverbes.AllAdverbeRepository;

internal static partial class AdverbeRepository
{
    public static readonly IReadOnlyList<string> O = new[]
    {
        Constants.Ouvertement,
        Constants.Obstinement,
        Constants.Obligatoirement,
        Constants.Ordinairement,
        Constants.Opiniatrement,
    };
}
