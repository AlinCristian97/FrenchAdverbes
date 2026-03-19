using System.Globalization;
using System.Text;
using System.Text.Json;

namespace FrenchAdverbes;

internal static class AdverbeRepository
{
    private static readonly Dictionary<string, WordMetadata> _cache = new();
    private static readonly Random _random = new();
    private const string BaseFolderName = "Sentences";
    private const string FileExtension = ".json";

    public static readonly IReadOnlyList<string> A = new[]
    {
        Constants.Alternativement,
        Constants.Attentivement,
        Constants.Apparemment,
        Constants.Alentour,
        Constants.Autrefois,
        Constants.Absolument,
        Constants.Ailleurs,
        Constants.AujourdHui,
        Constants.Aussi,
        Constants.Autour,
        Constants.Avant,
        Constants.AvantHier,
        Constants.Apres,
        Constants.Aucunement,
    };

    public static readonly IReadOnlyList<string> B = new[]
    {
        Constants.Beaucoup,
        Constants.Bien,
    };

    public static readonly IReadOnlyList<string> C = new[]
    {
        Constants.Combien,
        Constants.Certainement,
        Constants.Completement,
        Constants.Comment,
        Constants.Certes,
    };

    public static readonly IReadOnlyList<string> D = new[]
    {
        Constants.Desormais,
        Constants.Debout,
        Constants.Dedans,
        Constants.Dehors,
        Constants.Devant,
        Constants.Derriere,
        Constants.Deja,
        Constants.Dorenavant,
        Constants.Directement,
    };

    public static readonly IReadOnlyList<string> E = new[]
    {
        Constants.Extremement,
        Constants.Encore,
        Constants.Enfin,
        Constants.Egalement,
        Constants.Evidemment,
    };

    public static readonly IReadOnlyList<string> F = new[]
    {
        Constants.Forcement,
        Constants.Futilement,
        Constants.Finalement,
        Constants.Fixement,
    };

    public static readonly IReadOnlyList<string> I = new[]
    {
        Constants.Immediatement,
    };

    public static readonly IReadOnlyList<string> J = new[]
    {
        Constants.Jadis,
        Constants.Juste,
        Constants.Joyeusement,
        Constants.Justement,
    };

    public static readonly IReadOnlyList<string> L = new[]
    {
        Constants.Loin,
        Constants.Lors,
        Constants.Lateralement,
        Constants.Litteralement,
        Constants.Legerement,
        Constants.Largement,
    };

    public static readonly IReadOnlyList<string> M = new[]
    {
        Constants.Meme,
        Constants.Malheureusement,
        Constants.Maintenant,
        Constants.Mieux,
        Constants.Moins,
    };

    public static readonly IReadOnlyList<string> N = new[]
    {
        Constants.Notamment,
        Constants.Neanmoins,
    };

    public static readonly IReadOnlyList<string> P = new[]
    {
        Constants.Proprement,
        Constants.Presque,
        Constants.Pourtant,
        Constants.Pres,
        Constants.Parfois,
        Constants.Partout,
        Constants.Peu,
        Constants.Plus,
        Constants.Pourquoi,
        Constants.Puis,
        Constants.Parfaitement,
        Constants.PeutEtre,
        Constants.Probablement,
        Constants.Partiellement,
        Constants.Pratiquement,
        Constants.Precedemment,
        Constants.Precisement,
        Constants.Precieusement,
        Constants.Principalement,
        Constants.Progressivement,
        Constants.Publiquement,
        Constants.Patiemment,
        Constants.Prudemment,
        Constants.Profondement,
        Constants.Ponderement,
        Constants.Purement,
        Constants.Passionnement,
        Constants.Puissamment,
    };

    public static readonly IReadOnlyList<string> Q = new[]
    {
        Constants.Quand,
    };

    public static readonly IReadOnlyList<string> R = new[]
    {
        Constants.Rapidement,
    };

    public static readonly IReadOnlyList<string> S = new[]
    {
        Constants.Surtout,
        Constants.Super,
        Constants.Seulement,
        Constants.Souvent,
        Constants.Subitement,
        Constants.Soudain,
        Constants.Soigneusement,
    };

    public static readonly IReadOnlyList<string> T = new[]
    {
        Constants.Totalement,
        Constants.Tres,
        Constants.Tranquillement,
        Constants.Tard,
        Constants.Tant,
    };

    public static readonly IReadOnlyList<string> U = new[]
    {
        Constants.Uniquement,
    };

    public static readonly IReadOnlyList<string> V = new[]
    {
        Constants.Vraiment,
        Constants.Vite,
    };

    // Super-list that contains every letter list
    public static readonly IReadOnlyList<string> All = A
        .Concat(B)
        .Concat(C)
        .Concat(D)
        .Concat(E)
        .Concat(F)
        .Concat(I)
        .Concat(J)
        .Concat(L)
        .Concat(M)
        .Concat(N)
        .Concat(P)
        .Concat(Q)
        .Concat(R)
        .Concat(S)
        .Concat(T)
        .Concat(U)
        .Concat(V)
        .ToArray();

    private static IReadOnlyDictionary<char, IReadOnlyList<string>> BuildLetterMap()
    {
        return new Dictionary<char, IReadOnlyList<string>>(18)
        {
            [Constants.Alternativement[0]] = A,
            [Constants.Beaucoup[0]] = B,
            [Constants.Combien[0]] = C,
            [Constants.Debout[0]] = D,
            [Constants.Encore[0]] = E,
            [Constants.Finalement[0]] = F,
            [Constants.Immediatement[0]] = I,
            [Constants.Jadis[0]] = J,
            [Constants.Loin[0]] = L,
            [Constants.Maintenant[0]] = M,
            [Constants.Notamment[0]] = N,
            [Constants.Proprement[0]] = P,
            [Constants.Quand[0]] = Q,
            [Constants.Rapidement[0]] = R,
            [Constants.Super[0]] = S,
            [Constants.Totalement[0]] = T,
            [Constants.Uniquement[0]] = U,
            [Constants.Vraiment[0]] = V,
        };
    }

    public static bool TryGetRandomByLetter(char letter, out string? result)
    {
        result = null;
        var key = char.ToLowerInvariant(letter);

        if (!BuildLetterMap().TryGetValue(key, out var list) || list == null || list.Count == 0)
        {
            return false;
        }

        result = list[Random.Shared.Next(list.Count)];
        return true;
    }

    public static bool TryGetRandom(out string? result)
    {
        result = null;

        if (All is null || All.Count == 0)
        {
            return false;
        }

        result = All[Random.Shared.Next(All.Count)];
        return true;
    }

    public static List<string> GetSentencesForWord(string word, int count = Constants.NumberOfRandomExampleSentences)
    {
        if (string.IsNullOrWhiteSpace(word) || count <= 0)
            return new List<string>();

        var adverbe = word.ToLower().Trim();

        if (!_cache.ContainsKey(adverbe))
        {
            LoadAdverbeJson(adverbe);
        }

        if (_cache.TryGetValue(adverbe, out var metadata) && metadata?.Sentences != null && metadata.Sentences.Any())
        {
            return metadata.Sentences
                .OrderBy(_ => _random.Next())
                .Take(count)
                .ToList();
        }

        return new List<string>();
    }

    public static string GetDescriptionForWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return string.Empty;

        var adverbe = word.ToLower().Trim();

        if (!_cache.ContainsKey(adverbe))
            LoadAdverbeJson(adverbe);

        if (_cache.TryGetValue(adverbe, out var metadata) && !string.IsNullOrWhiteSpace(metadata.Description))
            return metadata.Description.Trim();

        return string.Empty;
    }

    private static void LoadAdverbeJson(string adverbe)
    {
        // Defensive default
        _cache[adverbe] = new WordMetadata();

        if (string.IsNullOrWhiteSpace(adverbe))
            return;

        // First letter folder (A, B, C, ...)
        char firstLetter = RemoveDiacritics(adverbe[0]).ToString().ToUpperInvariant()[0];

        // Base folder (runtime)
        string baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BaseFolderName);

        if (!Directory.Exists(baseDir))
        {
            // Go up from bin/... to project root
            string projectRoot = Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..")
            );

            baseDir = Path.Combine(projectRoot, BaseFolderName);
        }

        if (!Directory.Exists(baseDir))
            return;

        // Sentences/A/alternativement.json
        string adverbeFolder = Path.Combine(baseDir, firstLetter.ToString());
        string filePath = Path.Combine(adverbeFolder, $"{adverbe}{FileExtension}");

        if (!File.Exists(filePath))
            return;

        try
        {
            string jsonContent = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var metadata = JsonSerializer.Deserialize<WordMetadata>(jsonContent, options)
                           ?? new WordMetadata();

            metadata.Description ??= string.Empty;

            metadata.Sentences = (metadata.Sentences ?? new List<string>())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim())
                .ToList();

            _cache[adverbe] = metadata;
        }
        catch
        {
            // keep default empty metadata
            _cache[adverbe] = new WordMetadata();
        }
    }

    private static char RemoveDiacritics(char c)
    {
        string normalized = c.ToString().Normalize(NormalizationForm.FormD);
        foreach (char ch in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                return ch;
        }
        return c;
    }
}
