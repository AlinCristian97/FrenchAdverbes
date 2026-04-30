# Add a French Adverbe

## Trigger

The user provides a French adverbe to add to the project.

## Input

- **Adverbe value**: the French word itself (e.g. `gracieusement`, `également`).
- **Constant name**: the C# identifier to use. If not provided, derive it from the adverbe value by removing diacritics and using PascalCase (e.g. `également` → `Egalement`, `peut-être` → `PeutEtre`, `aujourd'hui` → `AujourdHui`).

## Steps

Follow **all three steps** in order. Do not skip any step.

### 1. Add the constant to the split `Constants` partial file

- Edit: `FrenchAdverbes\AllConstants\Constants.{LETTER}.cs`
- `{LETTER}` = uppercase ASCII first letter of the adverbe (strip diacritics: `é` → `E`, `à` → `A`, etc.).
- Add the constant at the end of the class body:

```csharp
public const string <ConstantName> = "<adverbe value>";
```

Rules:
- Constant name: PascalCase, no accents, no hyphens, no apostrophes.
- Value: lowercase adverbe, all accents and special characters preserved.

### 2. Add the constant to the split `AdverbeRepository` partial file

- Edit: `FrenchAdverbes\AllAdverbeRepository\AdverbeRepository.{LETTER}.cs`
- `{LETTER}` = same uppercase ASCII first letter as above.
- Append `Constants.<ConstantName>,` at the **end** of the array, just before the closing `};`.

> No other changes are needed — the `All` super-list and `BuildLetterMap()` in `AdverbeRepository.main.cs` already aggregate the per-letter arrays automatically.

> **If the letter file has no list yet** (the file is an empty shell), add a new list:
> ```csharp
> public static readonly IReadOnlyList<string> {LETTER} = new[]
> {
>     Constants.<ConstantName>,
> };
> ```
> Then also update `AdverbeRepository.main.cs`:
> 1. Add `.Concat({LETTER})` to the `All` super-list in alphabetical order.
> 2. Add `[Constants.<ConstantName>[0]] = {LETTER},` to `BuildLetterMap()` in alphabetical order, and increment the dictionary capacity by 1.

### 3. Create the JSON sentence file

- Determine the folder letter by stripping diacritics from the first character and uppercasing it (e.g. `é` → `E`).
- Create the file at `FrenchAdverbes\Sentences\{Letter}\{adverbe value}.json`.
- The file name must exactly match the adverbe **value** (including any accented characters), with a `.json` extension.
- Use **UTF-8 encoding without BOM**.
- Use this structure:

```json
{
  "description": "<Two sentences in French describing the adverbe — what it means and how it is typically used, naturally in French, with the English translation in parentheses.>",
  "sentences": [
    "<sentence 1>",
    "<sentence 2>",
    "<sentence 3>",
    "<sentence 4>",
    "<sentence 5>"
  ]
}
```

#### JSON content rules

- **`description`**: Write exactly **2 French sentences**. The first sentence should explain the meaning of the adverbe naturally in French, with the English equivalent in parentheses.
- **`sentences`**: Provide exactly **5 French sentences** that use the adverbe naturally. Each sentence should be **at least 15 words long** — avoid very short or trivial sentences.
- The file must be encoded as **UTF-8 without BOM**.

## Validation

After completing all three steps, run a build to confirm there are no compilation errors.

## Example

Given the adverbe **`gracieusement`**:

**`Constants.G.cs`** — at the end of the class:

```csharp
public const string Gracieusement = "gracieusement";
```

**`AdverbeRepository.G.cs`** — append to the `G` array (or create it if the file is an empty shell):

```csharp
public static readonly IReadOnlyList<string> G = new[]
{
    Constants.Gracieusement,
};
```

If `G` is newly created, also update `AdverbeRepository.main.cs`:
- Add `.Concat(G)` between `.Concat(F)` and `.Concat(I)` in `All`.
- Add `[Constants.Gracieusement[0]] = G,` to `BuildLetterMap()` and increment the dictionary capacity by 1.

**`Sentences\G\gracieusement.json`**:

```json
{
  "description": "« Gracieusement » (English: gracefully, graciously) est un adverbe qui décrit une action accomplie avec élégance, légèreté ou bienveillance. Il peut qualifier aussi bien un geste physique qu'un acte de générosité ou de politesse.",
  "sentences": [
    "La danseuse étoile s'est inclinée gracieusement devant le public qui l'acclamait avec une ferveur extraordinaire.",
    "Il a gracieusement accepté de céder sa place à la vieille dame qui peionait à se tenir debout dans le wagon bondé.",
    "La directrice a gracieusement remercié chacun des bénévoles pour leur engagement sans faille tout au long de l'événement.",
    "L'oiseau planait gracieusement au-dessus des vagues, indifférent au vent violent qui soufflait depuis le large.",
    "Elle a gracieusement décliné l'invitation en expliquant qu'elle avait déjà pris d'autres engagements pour ce soir-là."
  ]
}
```
