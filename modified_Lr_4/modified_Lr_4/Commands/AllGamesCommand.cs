using modified_Lr_4.Entity.GameEntities;
using modified_Lr_4.Service;

namespace modified_Lr_4.Commands;

public class AllGamesCommand : ICommand
{
    private readonly GameService _gameService;

    public AllGamesCommand(GameService gameService)
    {
        _gameService = gameService;
    }

    public void Execute()
    {
        Console.WriteLine("\nList of all games:");
        foreach (GameEntity game in _gameService.ReadGames())
        {
            PrintGameInfo(game);
        }
    }

    private void PrintGameInfo(GameEntity game)
    {
        String result = _gameService.IsPlayerWinner(game.PlayerId, game.Id) ? "Win" : "Lose";
        Console.WriteLine($"Game #{game.Id} - Result: {result}, Rating Change: {game.ChangeOfRating}, Game Type: " +
                          $"{_gameService.GetGameTypeName(game)}");
    }
}