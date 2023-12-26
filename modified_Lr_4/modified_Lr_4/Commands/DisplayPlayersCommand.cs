using modified_Lr_4.Entity;
using modified_Lr_4.Service;

namespace modified_Lr_4.Commands;

public class DisplayPlayersCommand : ICommand
{
    private readonly GameService _gameService;

    public DisplayPlayersCommand(GameService gameService)
    {
        _gameService = gameService;
    }

    public void Execute()
    {
        Console.WriteLine("List of players:");
        foreach (PlayerEntity player in _gameService.ReadAccounts())
        {
            Console.WriteLine($"{player.Id}. {player.UserName} - Rating: {player.CurrentRating}");
        }
    }
}