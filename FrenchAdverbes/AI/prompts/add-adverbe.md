# Add a French Adverbe

## Trigger

The user provides a French adverbe to add to the project.

## Steps

Follow **all three steps** in order. Do not skip any step.

### 1. Add the constant to `FrenchAdverbes\Constants.cs`

- Find the correct **letter section** (e.g. `// A`, `// B`, …) based on the adverbe's **base first letter** (strip diacritics: é→e, ê→e, à→a, etc.).
- If no section exists for that letter, create one with a `// X` comment, placed in alphabetical order among the existing sections. Keep it **before** the `// Formatting` section at the bottom.
- Add a `public const string` entry inside that section.
- **Property name rules:**
  - PascalCase, **no accented characters** (e.g. `Deja` not `Déjà`, `Evidemment` not `Évidemment`).
  - If the adverbe contains a hyphen or apostrophe, remove the separator and PascalCase each part (e.g. `aujourd'hui` → `AujourdHui`, `peut-être` → `PeutEtre`).
- **Value:** the adverbe exactly as written in French, lowercase, with all accents and special characters preserved (e.g. `"déjà"`, `"peut-être"`).

### 2. Add the constant reference to `FrenchAdverbes\AdverbeRepository.cs`

- Find the matching **letter list** (e.g. `public static readonly IReadOnlyList<string> A = new[] { … };`).
- Append `Constants.<PropertyName>` to the end of that list (before the closing `};`), keeping the trailing comma style.
- If no letter list exists for this letter:
  1. Create a new `public static readonly IReadOnlyList<string> X = new[] { … };` field in alphabetical position among the existing letter lists.
  2. Add `.Concat(X)` to the `All` super-list, in alphabetical order among the existing `.Concat(…)` calls.
  3. Add a `[Constants.<SomeConstantStartingWithX>[0]] = X,` entry to `BuildLetterMap()` (pick any constant from the new letter whose value starts with the **unaccented** base letter), and increment the dictionary capacity accordingly.

### 3. Create the JSON file at `FrenchAdverbes\Sentences\{Letter}\{value}.json`

- `{Letter}` is the **uppercase base letter** (diacritics stripped, e.g. `E` for `également`).
- `{value}` is the full adverbe value in lowercase with accents (the constant's value), e.g. `également.json`.
- Use **UTF-8 encoding without BOM**.
- Use the following structure, replacing every occurrence of `{adverbe}` with the adverbe value:

```json
{
  "description": "En français, l'adverbe « {adverbe} » sert à caractériser et à préciser la nature d'un nom auquel il se rapporte dans une phrase. Son usage est répandu dans divers registres de langue, du familier au soutenu, et il contribue à la richesse descriptive du discours.",
  "sentences": [
    "L'architecte a présenté son projet devant le jury, qui l'a trouvé {adverbe}, saluant notamment son approche originale et respectueuse de l'environnement.",
    "Les critiques ont unanimement décrit le nouveau spectacle comme étant {adverbe}, soulignant la qualité exceptionnelle de la mise en scène.",
    "En observant attentivement le comportement des participants, le chercheur a constaté que leur état émotionnel était systématiquement {adverbe}.",
    "Le guide touristique a expliqué que ce monument historique du dix-huitième siècle était considéré comme {adverbe} par les visiteurs du monde entier.",
    "Le photographe a su capturer un instant précieux que les visiteurs de l'exposition ont unanimement qualifié de {adverbe}."
  ]
}
```

## Validation

After completing all three steps, run a build to confirm there are no compilation errors.

## Example

Given the adverbe **`gracieusement`**:

1. **Constants.cs** — add under `// G` (new section, before `// I`):
   ```csharp
   // G
   public const string Gracieusement = "gracieusement";
   ```

2. **AdverbeRepository.cs** — create list, update `All`, update `BuildLetterMap()`:
   ```csharp
   public static readonly IReadOnlyList<string> G = new[]
   {
       Constants.Gracieusement,
   };
   ```
   Add `.Concat(G)` between `.Concat(F)` and `.Concat(I)` in `All`.
   Add `[Constants.Gracieusement[0]] = G,` to `BuildLetterMap()` and update the capacity from `18` to `19`.

3. **JSON** — create `FrenchAdverbes\Sentences\G\gracieusement.json` with the template above, replacing `{adverbe}` with `gracieusement`.
