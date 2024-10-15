using System.Text;

Console.WriteLine("Welcome to Hangman!");
Hangman game = new Hangman("example", 6);

while (game.IsInProgress)
{
    Console.WriteLine("Current word: " + game.MaskedWord);
    Console.WriteLine("Incorrect guesses: " + string.Join(", ", game.IncorrectGuesses));
    Console.WriteLine("Enter a letter to guess:");
    char guess = Console.ReadLine()[0];

    string result = game.Guess(guess);
    Console.WriteLine(result);
}
