# Add a French Adverbe

## Trigger

The user provides a French adverbe to add to the project.

## Input

- **Adverbe value**: the French word itself (e.g. `gracieusement`, `également`).
- **Constant name**: derived from the value — PascalCase, no diacritics, no hyphens/apostrophes (e.g. `également` → `Egalement`, `peut-être` → `PeutEtre`, `aujourd'hui` → `AujourdHui`).

## Steps

> **Order does not matter** — simply append each addition to the end. No alphabetical ordering required.

### 1. Add the constant — `AllConstants\Constants.{LETTER}.cs`

`{LETTER}` = uppercase ASCII first letter (strip diacritics: `é` → `E`, `à` → `A`, etc.).

Append at the end of the class body:

```csharp
public const string <ConstantName> = "<adverbe value>";
```

### 2. Add to the repository — `AllAdverbeRepository\AdverbeRepository.{LETTER}.cs`

Append at the end of the array, just before the closing `};`:

```csharp
Constants.<ConstantName>,
```

> **If the file is still an empty shell** (no list exists yet), create the list:
> ```csharp
> public static readonly IReadOnlyList<string> {LETTER} = new[]
> {
>     Constants.<ConstantName>,
> };
> ```
> Then update `AdverbeRepository.main.cs`:
> 1. Add `.Concat({LETTER})` to the `All` super-list in alphabetical order.
> 2. Add `[Constants.<ConstantName>[0]] = {LETTER},` to `BuildLetterMap()` in alphabetical order, and increment the dictionary capacity by 1.

### 3. Create the JSON sentence file

Path: `Sentences\{LETTER}\{adverbe value}.json` — filename must match the value exactly, UTF-8 without BOM.

```json
{
  "description": "<2 French sentences: what the adverbe means and how it is used, with the English translation in parentheses.>",
  "sentences": [
    "<sentence 1>",
    "<sentence 2>",
    "<sentence 3>",
    "<sentence 4>",
    "<sentence 5>"
  ]
}
```

- **`description`**: exactly 2 French sentences, English equivalent in parentheses.
- **`sentences`**: exactly 5 natural French sentences, each at least 15 words long.

## Validation

Run a build and confirm there are no compilation errors.

## Example

Given **`gracieusement`**:

**`Constants.G.cs`**:
```csharp
public const string Gracieusement = "gracieusement";
```

**`AdverbeRepository.G.cs`** (empty shell → new list):
```csharp
public static readonly IReadOnlyList<string> G = new[]
{
    Constants.Gracieusement,
};
```
Also update `AdverbeRepository.main.cs`: add `.Concat(G)` to `All`, add `[Constants.Gracieusement[0]] = G,` to `BuildLetterMap()`, increment capacity by 1.

**`Sentences\G\gracieusement.json`**:
```json
{
  "description": "« Gracieusement » (English: gracefully, graciously) est un adverbe qui décrit une action accomplie avec élégance ou bienveillance. Il peut qualifier aussi bien un geste physique qu'un acte de générosité ou de politesse.",
  "sentences": [
    "La danseuse étoile s'est inclinée gracieusement devant le public qui l'acclamait avec une ferveur extraordinaire.",
    "Il a gracieusement accepté de céder sa place à la vieille dame qui peinait à se tenir debout dans le wagon bondé.",
    "La directrice a gracieusement remercié chacun des bénévoles pour leur engagement sans faille tout au long de l'événement.",
    "L'oiseau planait gracieusement au-dessus des vagues, indifférent au vent violent qui soufflait depuis le large.",
    "Elle a gracieusement décliné l'invitation en expliquant qu'elle avait déjà pris d'autres engagements pour ce soir-là."
  ]
}
```
