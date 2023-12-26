using modified_Lr_4.GameAccounts;

namespace modified_Lr_4.Entity;

public class PlayerEntity
{
    public int Id { get; set; }
    public string? UserName { get; }
    public decimal CurrentRating { get; set; }
    public GameAccount GameAccount { get; }

    public PlayerEntity(GameAccount gameAccount)
    {
        UserName = gameAccount.UserName;
        CurrentRating = gameAccount.CurrentRating;
        GameAccount = gameAccount;
    }
}