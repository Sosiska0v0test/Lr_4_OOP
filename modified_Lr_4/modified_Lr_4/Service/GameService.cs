using modified_Lr_4.Entity;
using modified_Lr_4.Entity.GameEntities;
using modified_Lr_4.GameAccounts;
using modified_Lr_4.Repository.IRepository;

namespace modified_Lr_4.Service;

public class GameService : IGameService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IGameRepository _gameRepository;

    public GameService(IPlayerRepository playerRepository, IGameRepository gameRepository)
    {
        _playerRepository = playerRepository;
        _gameRepository = gameRepository;
    }

    public void CreateGame(GameEntity game)
    {
        _gameRepository.CreateGame(game);
    }

    public IEnumerable<GameEntity> ReadPlayerGamesByPlayerId(int playerId)
    {
        return _gameRepository.ReadAllGames().Where(g => g.PlayerId == playerId);
    }

    public IEnumerable<GameEntity> ReadGames()
    {
        return _gameRepository.ReadAllGames();
    }

    public void CreateAccount(PlayerEntity player)
    {
        _playerRepository.CreatePlayer(player);
    }

    public IEnumerable<PlayerEntity> ReadAccounts()
    {
        return _playerRepository.ReadAccounts();
    }

    public bool IsPlayerWinner(int playerId, int gameId)
    {
        GameEntity game = _gameRepository.ReadGameById(gameId);
        PlayerEntity player = _playerRepository.ReadAccountById(playerId);
        Random random = new Random();
        bool isWinner = random.Next(2) == 0;

        decimal changeOfRating = game.ChangeOfRating;

        if (isWinner)
        {
            player.CurrentRating += CalculateWinPoints(player, changeOfRating);
        }
        else
        {
            player.CurrentRating -= CalculateLosePoints(player, changeOfRating);

            if (player.CurrentRating < 1)
            {
                player.CurrentRating = 1;
            }
        }

        _playerRepository.UpdateRating(player.Id, player.CurrentRating);

        return isWinner;
    }

    private int _consecutiveWins;

    public decimal CalculateWinPoints(PlayerEntity player, decimal changeOfRating)
    {
        if (player.GameAccount is not WinningStreakGameAccount) return changeOfRating;

        _consecutiveWins++;
        if (_consecutiveWins >= 3)
        {
            return changeOfRating + 100;
        }

        return changeOfRating;
    }

    public decimal CalculateLosePoints(PlayerEntity player, decimal changeOfRating)
    {
        switch (player.GameAccount)
        {
            case ReducedLossGameAccount:
                return changeOfRating / 2;

            case WinningStreakGameAccount:
                _consecutiveWins = 0;
                break;
        }

        return changeOfRating;
    }

    public decimal GetPlayerRating(int playerId)
    {
        PlayerEntity player = _playerRepository.ReadAccountById(playerId);
        return player.CurrentRating;
    }

    public string GetGameTypeName(GameEntity game)
    {
        return game switch
        {
            StandardGameEntity => "Standard Game",
            TrainingGameEntity => "Training Game",
            RandomRatingGameEntity => "Random Rating Game",
            _ => "Unknown Game Type"
        };
    }
}