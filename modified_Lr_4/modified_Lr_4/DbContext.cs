using modified_Lr_4.Entity;
using modified_Lr_4.Entity.GameEntities;

namespace modified_Lr_4;

public class DbContext
{
    public List<PlayerEntity> Players { get; } = new();
    public List<GameEntity> Games { get; } = new();
}