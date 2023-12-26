using modified_Lr_4.Entity;

namespace modified_Lr_4.Repository.IRepository;

public interface IPlayerRepository
{
    void CreatePlayer(PlayerEntity player);
    PlayerEntity ReadPlayerById(int playerId);
    IEnumerable<PlayerEntity> ReadAllPlayers();

    void CreateAccount(PlayerEntity player);
    IEnumerable<PlayerEntity> ReadAccounts();
    PlayerEntity ReadAccountById(int playerId);
    void UpdateRating(int playerId, decimal newRating);
}