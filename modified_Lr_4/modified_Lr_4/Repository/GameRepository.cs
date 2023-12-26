using modified_Lr_4.Entity.GameEntities;
using modified_Lr_4.Repository.IRepository;

namespace modified_Lr_4.Repository;

public class GameRepository : IGameRepository
{
    private readonly List<GameEntity> _games;

    public GameRepository(List<GameEntity> games)
    {
        _games = games;
    }

    public void CreateGame(GameEntity game)
    {
        game.Id = _games.Count + 1;
        _games.Add(game);
    }

    public GameEntity ReadGameById(int gameId)
    {
        return _games.FirstOrDefault(g => g.Id == gameId) ?? throw new InvalidOperationException();
    }

    public IEnumerable<GameEntity> ReadAllGames()
    {
        return _games;
    }
}