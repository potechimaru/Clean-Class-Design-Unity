using UnityEngine;
using VContainer;


public interface IEnemyConfigFacade
{
    SlimeConfig GetSlimeConfig();
    TurtleConfig GetTurtleConfig();
}
public class EnemyConfigFacade: IEnemyConfigFacade
{
    [Inject] private SlimeConfig _slimeConfig;
    [Inject] private TurtleConfig _turtleConfig;
    public SlimeConfig GetSlimeConfig()
    {
        return _slimeConfig;
    }

    public TurtleConfig GetTurtleConfig()
    {
        return _turtleConfig;
    }
}
