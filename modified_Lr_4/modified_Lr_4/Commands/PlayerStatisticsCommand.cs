using modified_Lr_4.Entity;
using modified_Lr_4.Entity.GameEntities;
using modified_Lr_4.Service;

namespace modified_Lr_4.Commands;

public class PlayerStatisticsCommand : ICommand
{
    private readonly GameService _gameService;

    public PlayerStatisticsCommand(GameService gameService)
    {
        _gameService = gameService;
    }

    public void Execute()
    {
        Console.Write("\nEnter a player's name to view stats --> ");
        string? playerName = Console.ReadLine();

        PlayerEntity player = _gameService.ReadAccounts().
            FirstOrDefault(p => p.UserName != null && p.UserName.
                Equals(playerName, StringComparison.OrdinalIgnoreCase)) ?? throw new InvalidOperationException();

        PrintPlayerGamesInfo(player);
        Console.Write("\nDo you want to view information about another player? (y/n): ");
        string? response = Console.ReadLine();
        if (response != null && response.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            Execute();
        }
    }

    private void PrintPlayerGamesInfo(PlayerEntity player)
    {
        Console.WriteLine($"\nList of games for {player.UserName}:");
        foreach (GameEntity game in _gameService.ReadPlayerGamesByPlayerId(player.Id))
        {
            PrintGameInfo(game);
        }
    }

    private void PrintGameInfo(GameEntity game)
    {
        var result = _gameService.IsPlayerWinner(game.PlayerId, game.Id) ? "Win" : "Lose";
        Console.WriteLine($"Game #{game.Id} - Result: {result}, Rating Change: {game.ChangeOfRating}, " +
                          $"New Rating: {_gameService.GetPlayerRating(game.PlayerId)}, Game Type: " +
                          $"{_gameService.GetGameTypeName(game)}");
    }
}