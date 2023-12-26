namespace modified_Lr_4.Entity.GameEntities;

public class StandardGameEntity : GameEntity
{
    public StandardGameEntity(decimal changeOfRating, int playerId)
    {
        ChangeOfRating = changeOfRating;
        PlayerId = playerId;
    }
}