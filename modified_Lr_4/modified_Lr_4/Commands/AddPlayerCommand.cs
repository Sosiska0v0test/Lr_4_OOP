using modified_Lr_4.Entity;
using modified_Lr_4.GameAccounts;
using modified_Lr_4.Service;

namespace modified_Lr_4.Commands;

public class AddPlayerCommand : ICommand
{
    private readonly GameService _gameService;

    public AddPlayerCommand(GameService gameService)
    {
        _gameService = gameService;
    }

    public void Execute()
    {
        bool addAnotherPlayer = true;

        while (addAnotherPlayer)
        {
            Console.Write("Enter the player's name --> ");
            string? playerName = Console.ReadLine();

            Console.Write("Enter your initial rating --> ");
            if (int.TryParse(Console.ReadLine(), out int initialRating))
            {
                Console.WriteLine("\nSelect an account type:\n1. Standard\n2. ReducedLoss\n3. WinningStreak");
                if (int.TryParse(Console.ReadLine(), out int accountTypeChoice) && accountTypeChoice is >= 1 and <= 3)
                {
                    AccountType accountType = (AccountType)accountTypeChoice;
                    PlayerEntity newPlayer = CreatePlayer(playerName, initialRating, accountType);
                    _gameService.CreateAccount(newPlayer);
                    Console.WriteLine($"Player {newPlayer.UserName} added successfully.");

                    Console.Write("Would you like to add another player? (y/n): ");
                    string? addAnotherPlayerInput = Console.ReadLine();
                    if (addAnotherPlayerInput != null)
                        addAnotherPlayer = addAnotherPlayerInput.Equals("y", StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    Console.WriteLine("Incorrect choice of account type. Player addition canceled.");
                    addAnotherPlayer = false;
                }
            }
            else
            {
                Console.WriteLine("Incorrect rating format. Player addition canceled.");
                addAnotherPlayer = false;
            }
        }
    }

    private PlayerEntity CreatePlayer(string? playerName, int initialRating, AccountType accountType)
    {
        return accountType switch
        {
            AccountType.Standard => new PlayerEntity(new StandardGameAccount(playerName, initialRating)),
            AccountType.ReducedLoss => new PlayerEntity(new ReducedLossGameAccount(playerName, initialRating)),
            AccountType.WinningStreak => new PlayerEntity(new WinningStreakGameAccount(playerName, initialRating)),
            _ => throw new InvalidOperationException("Unsupported account type.")
        };
    }
}

public enum AccountType
{
    Standard = 1,
    ReducedLoss = 2,
    WinningStreak = 3
}