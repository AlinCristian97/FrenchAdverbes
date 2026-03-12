namespace FrenchAdverbes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choisissez une option :");
                Console.WriteLine("1) Mot aléatoire");
                Console.WriteLine("2) Mot aléatoire par lettre");
                Console.WriteLine("3) Entrer un mot");
                Console.WriteLine("Q) Quitter");

                var choice = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Entrée invalide.\n");
                    continue;
                }

                if (string.Equals(choice, "Q", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (choice == "1")
                {
                    if (AdverbeRepository.TryGetRandom(out var word))
                    {
                        DisplayWordWithMetadata(word);
                    }
                    else
                    {
                        Console.WriteLine("Aucun mot disponible.\n");
                    }

                    continue;
                }

                if (choice == "2")
                {
                    Console.Write("Entrez une lettre (a - z) : ");
                    var letterInput = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(letterInput) || !char.IsLetter(letterInput[0]))
                    {
                        Console.WriteLine("Lettre invalide.\n");
                        continue;
                    }

                    var letter = letterInput[0];
                    if (AdverbeRepository.TryGetRandomByLetter(letter, out var wordByLetter))
                    {
                        DisplayWordWithMetadata(wordByLetter);
                    }
                    else
                    {
                        Console.WriteLine($"Aucun mot trouvé pour la lettre '{char.ToLowerInvariant(letter)}'.\n");
                    }

                    continue;
                }

                if (choice == "3")
                {
                    Console.Write("Entrez le mot : ");
                    var userInput = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(userInput))
                    {
                        Console.WriteLine("Entrée invalide.\n");
                        continue;
                    }

                    var canonical = ResolveToCanonical(userInput);

                    var exists = AdverbeRepository.All
                        .Any(e => string.Equals(e, canonical, StringComparison.OrdinalIgnoreCase));

                    if (!exists)
                    {
                        Console.WriteLine("Le mot n'existe pas ou n'a pas été implémenté.\n");
                        continue;
                    }

                    // use repository's casing if available
                    var repoEntry = AdverbeRepository.All
                        .First(e => string.Equals(e, canonical, StringComparison.OrdinalIgnoreCase));

                    DisplayWordWithMetadata(repoEntry);

                    continue;
                }

                Console.WriteLine("Choix invalide.\n");
            }
        }

        private static void DisplayWordWithMetadata(string word, int exampleCount = Constants.NumberOfRandomExampleSentences)
        {
            Console.WriteLine(Constants.LongDivider);
            PrintColoredWord(word);
            Console.WriteLine();
            Console.WriteLine(Constants.ShortDivider);
            PrintDescription(word);
            Console.WriteLine(Constants.ShortDivider);
            PrintExampleSentences(word, exampleCount);
            Console.WriteLine(Constants.LongDivider);
        }

        private static void PrintColoredWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                Console.Write(word);
                return;
            }

            var previous = Console.ForegroundColor;

            Console.Write(Constants.GuillemetOuvrant + Constants.Space);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(word);
            Console.ResetColor();
            Console.Write(Constants.Space + Constants.GuillemetFermant);

            Console.ForegroundColor = previous;
        }

        private static void PrintExampleSentences(string word, int count = Constants.NumberOfRandomExampleSentences)
        {
            if (string.IsNullOrWhiteSpace(word) || count <= 0)
                return;

            var sentences = AdverbeRepository.GetSentencesForWord(word, count);
            if (sentences == null || sentences.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("***JSON SENTENCES MISSING***");
                Console.ResetColor();
            }

            var previous = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (var s in sentences)
            {
                Console.WriteLine($"{Constants.GuillemetOuvrant}{Constants.Space}{s}{Constants.Space}{Constants.GuillemetFermant}");
            }
            Console.ForegroundColor = previous;
        }

        private static void PrintDescription(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return;

            var description = AdverbeRepository.GetDescriptionForWord(word);
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("***JSON DESCRIPTION MISSING***");
                Console.ResetColor();
            }

            var previous = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(description);
            Console.ForegroundColor = previous;
        }

        private static string ResolveToCanonical(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input ?? string.Empty;

            // For adverbes we expect the adverbe itself.
            return input.Trim().ToLowerInvariant();
        }
    }
}
