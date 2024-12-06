using System.Security.Cryptography;
using System.Text;

string randomNumber = "";
string guess;
int numberOfTries = 0;
const int MAX_NUMBER_OF_TRIES = 10;

Console.WriteLine("Welcome to Mike's MasterMind!");

// determine 4 random numbers 1-6
// store as 1 string of 4 digits
for (int i = 0; i < 4; i++)
{
    randomNumber += (RandomNumberGenerator.GetInt32(6) + 1).ToString();
}

do
{
    // increment number of tries
    numberOfTries++;

    // get the guess and validate it
    do
    {
        Console.WriteLine($"Enter guess #{numberOfTries}: ");
        guess = Console.ReadLine()?.Trim() ?? "";
    }
    while (!GuessIsValid(guess));

    // determine the hint
    string hint = GetHint(randomNumber, guess);

    // determine if the guess is correct
    if (randomNumber == guess)
    {
        Console.WriteLine("You Won!");
    }
    else
    {
        // guess not correct
        // provide hint if # of tries < MAX
        if (numberOfTries < MAX_NUMBER_OF_TRIES)
        {
            Console.WriteLine($"Try again!  Hint: {hint}");
        }
        else
        {
            // otherwise, notify user of loss, provide the answer, exit do-while loop below, end the game
            Console.WriteLine($"You lost!  The correct answer is {randomNumber}");
        }
    }
}
while (randomNumber != guess && numberOfTries < MAX_NUMBER_OF_TRIES);

/// <summary>
/// Determine Hint from Random Number and Guess
/// </summary>
string GetHint(string randomNumber, string guess)
{
    string hint = "";
    for (int i = 0; i < 4; i++)
    {
        if (randomNumber.Substring(i, 1) == guess.Substring(i, 1))
        {
            hint = $"+{hint}";
        }
        else
        {
            if (randomNumber.Contains(guess.Substring(i, 1)))
            {
                hint += "-";
            }
        }
    }
    return hint;
}

/// <summary>
/// Determine if guess is valid.
/// </summary>
bool GuessIsValid(string guess)
{
    StringBuilder invalidMessages = new();

    if (guess.Length != 4)
    {
        invalidMessages.AppendLine("Guess must be 4 digits long.");
    }

    if (!int.TryParse(guess, out _))
    {
        invalidMessages.AppendLine("Guess must be a number.");
    }

    if (invalidMessages.Length > 0)
    {
        Console.WriteLine(invalidMessages);
        return false;
    }

    // run this check only if guess is a 4 digit number (as determined above)
    for (int i = 0; i < 4; i++)
    {
        if (guess[i] < '1' || guess[i] > '6')
        {
            Console.WriteLine("Each digit must be a number from 1 to 6.");
            return false;
        }
    }

    return true;
}