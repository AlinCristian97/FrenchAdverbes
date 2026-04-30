using FrenchAdverbes.AllConstants;

namespace FrenchAdverbes.AllAdverbeRepository;

internal static partial class AdverbeRepository
{
    public static readonly IReadOnlyList<string> C = new[]
    {
        Constants.Combien,
        Constants.Certainement,
        Constants.Completement,
        Constants.Comment,
        Constants.Certes,
        Constants.Clairement,
        Constants.Constamment,
        Constants.Couramment,
        Constants.Consciemment,
        Constants.Curieusement,
    };
}
