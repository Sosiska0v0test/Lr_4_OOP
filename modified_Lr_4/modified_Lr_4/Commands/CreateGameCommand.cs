using modified_Lr_4.Entity;
using modified_Lr_4.Entity.GameEntities;
using modified_Lr_4.Service;

namespace modified_Lr_4.Commands;

public class CreateGameCommand : ICommand
{
    private readonly GameService _gameService;

    public CreateGameCommand(GameService gameService)
    {
        _gameService = gameService;
    }

    public void Execute()
    {
        do
        {
            Console.Write("\nEnter a player name for the game--> ");
            string? playerName = Console.ReadLine();
            PlayerEntity player = _gameService.ReadAccounts().FirstOrDefault(p => p.UserName != null &&
                p.UserName.Equals(playerName, StringComparison.OrdinalIgnoreCase)) ??
                                  throw new InvalidOperationException();

            Console.WriteLine("Select the type of game:");
            Console.WriteLine("1. Standard Game");
            Console.WriteLine("2. Training Game");
            Console.WriteLine("3. Random Rating Game");

            if (int.TryParse(Console.ReadLine(), out int gameTypeChoice))
            {
                GameEntity game;

                switch (gameTypeChoice)
                {
                    case 1:
                        Console.Write("Enter a rating for a standard game --> ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal standardGameRating))
                        {
                            game = new StandardGameEntity(standardGameRating, player.Id);
                        }
                        else
                        {
                            Console.WriteLine("Invalid rating input.");
                            return;
                        }
                        break;
                    case 2:
                        game = new TrainingGameEntity(player.Id);
                        break;
                    case 3:
                        game = new RandomRatingGameEntity(player.Id);
                        break;
                    default:
                        Console.WriteLine("Incorrect choice of game type.");
                        return;
                }

                _gameService.CreateGame(game);
                Console.WriteLine($"The game is created! Game ID: {game.Id}");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }

            Console.Write("Want to create another game? (y/n): ");
        } while (Console.ReadLine() == "y");
    }
}