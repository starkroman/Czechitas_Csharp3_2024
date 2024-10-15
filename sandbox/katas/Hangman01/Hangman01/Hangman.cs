using System.Text;
public class Hangman
{
    private string secretWord;
    private HashSet<char> guessedLetters;
    private List<char> incorrectGuesses;
    private int incorrectGuessLimit;
    public bool IsInProgress { get; private set; }

    public Hangman(string word, int maxIncorrectGuesses)
    {
        secretWord = word.ToUpper();
        guessedLetters = new HashSet<char>();
        incorrectGuesses = new List<char>();
        incorrectGuessLimit = maxIncorrectGuesses;
        IsInProgress = true;
    }

    public string MaskedWord
    {
        get
        {
            StringBuilder maskedWord = new StringBuilder();
            foreach (char c in secretWord)
            {
                if (guessedLetters.Contains(c))
                {
                    maskedWord.Append(c);
                }
                else
                {
                    maskedWord.Append('_');
                }
            }
            return maskedWord.ToString();
        }
    }

    public string Guess(char letter)
    {
        letter = char.ToUpper(letter);

        if (!char.IsLetter(letter))
        {
            return "Invalid guess. Please enter a letter A-Z.";
        }

        if (guessedLetters.Contains(letter) || incorrectGuesses.Contains(letter))
        {
            return "You already guessed that letter.";
        }

        if (secretWord.Contains(letter))
        {
            guessedLetters.Add(letter);
            if (IsWordGuessed())
            {
                IsInProgress = false;
                return "Congratulations! You've won!";
            }
            return $"Good guess! {MaskedWord}";
        }
        else
        {
            incorrectGuesses.Add(letter);
            if (incorrectGuesses.Count >= incorrectGuessLimit)
            {
                IsInProgress = false;
                return $"Game over! The word was: {secretWord}";
            }
            return $"Incorrect guess. You have {incorrectGuessLimit - incorrectGuesses.Count} guesses left.";
        }
    }

    private bool IsWordGuessed()
    {
        foreach (char c in secretWord)
        {
            if (!guessedLetters.Contains(c))
            {
                return false;
            }
        }
        return true;
    }

    public List<char> IncorrectGuesses
    {
        get { return incorrectGuesses; }
    }
}
