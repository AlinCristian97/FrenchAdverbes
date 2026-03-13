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
  "description": "L'adverbe « comment » signifie 'how' en anglais. C'est un adverbe interrogatif qui porte sur la manière ou le moyen. Il s'emploie dans les questions directes (« Comment vas-tu ? ») et indirectes (« Je ne sais pas comment faire »). Il peut aussi exprimer la surprise (« Comment ! »).",
  "sentences": [
    "Comment allez-vous depuis la dernière fois ?",
    "Je ne comprends pas comment il a réussi cet examen.",
    "Comment ça se fait que tu sois déjà là ?",
    "Explique-moi comment fonctionne cette machine.",
    "Comment ! Tu n'es pas encore prêt ?"
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

3. **JSON** — create `FrenchAdverbes\Sentences\G\gracieusement.json` with the template above, giving proper description and sentences in natural french.
