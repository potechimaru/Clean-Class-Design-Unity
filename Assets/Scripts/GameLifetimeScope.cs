using VContainer;
using VContainer.Unity;
using UnityEngine;
using State.GameState;
using State.PlayerState;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerView _playerView;

    [SerializeField] private SlimeFactory _slimeFactory;
    [SerializeField] private TurtleFactory _turtleFactory;

    [SerializeField] private SlimePool _slimePool;
    [SerializeField] private TurtlePool _turtlePool;    

    [SerializeField] private WaveRunner _waveRunner;

    [SerializeField] private SlimeConfig _slimeConfig;
    [SerializeField] private TurtleConfig _turtleConfig;
    [SerializeField] private WaveConfig _waveConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        // GameState
        builder.RegisterEntryPoint<GameStateMachine>(Lifetime.Singleton)
                .As<State.GameState.IStateController>();

        // PlayerState
        builder.RegisterEntryPoint<PlayerStateRunner>(Lifetime.Singleton)
               .As<State.PlayerState.IStateController>();

        builder.Register<EnemyManager>(Lifetime.Singleton)
               .As<ITickable>();

        builder.Register<PlayerModel>(Lifetime.Singleton);
        builder.RegisterComponent(_playerView);
        builder.Register<PlayerController>(Lifetime.Singleton);
        builder.Register<InputService>(Lifetime.Singleton);

        builder.Register<PlayerMVCFacade>(Lifetime.Singleton);

        // Factory
        builder.RegisterComponent(_slimeFactory);
        builder.RegisterComponent(_turtleFactory);

        // Pool
        builder.RegisterComponent(_slimePool);
        builder.RegisterComponent(_turtlePool);

        // Wave 
        builder.RegisterComponent(_waveRunner);

        // Config
        builder.Register<SlimeConfig>(Lifetime.Singleton);
        builder.Register<TurtleConfig>(Lifetime.Singleton);

    }

}
