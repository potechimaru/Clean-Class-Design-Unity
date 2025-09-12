using State.EnemyState;
using State.GameState;
using State.PlayerState;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerView _playerView;

    [SerializeField] private SlimeFactory _slimeFactory;
    [SerializeField] private TurtleFactory _turtleFactory;

    [SerializeField] private SlimePool _slimePool;
    [SerializeField] private TurtlePool _turtlePool;
    [SerializeField] private DamageTextPool _damageTextPool;

    [SerializeField] private SlimeConfig _slimeConfig;
    [SerializeField] private TurtleConfig _turtleConfig;
    [SerializeField] private WaveConfig _waveConfig;

    [SerializeField] private CameraFollow _cameraFollow;


    protected override void Configure(IContainerBuilder builder)
    {
        // GameState
        builder.RegisterEntryPoint<GameStateMachine>(Lifetime.Singleton)
               .As<State.GameState.IStateController>();

        // PlayerState
        builder.RegisterEntryPoint<PlayerStateRunner>(Lifetime.Singleton)
               .As<State.PlayerState.IStateController>();

        builder.Register<EnemyManager>(Lifetime.Singleton)
               .As<ITickable>().AsSelf();

        // EnemyState
        builder.Register<EnemyStateRunner>(Lifetime.Transient).As<State.EnemyState.IStateController>().AsSelf();

        builder.Register<EnemyIdleState>(Lifetime.Transient);
        builder.Register<EnemyWalkState>(Lifetime.Transient);
        builder.Register<EnemyAttackState>(Lifetime.Transient);
        builder.Register<EnemyDeadState>(Lifetime.Transient);

        builder.Register<PlayerModel>(Lifetime.Singleton);
        builder.RegisterComponent(_playerView);
        builder.Register<PlayerController>(Lifetime.Singleton).As<ITickable>().AsSelf(); ;
        builder.Register<InputService>(Lifetime.Singleton);

        builder.Register<PlayerMVCFacade>(Lifetime.Singleton);

        // Factory
        builder.RegisterComponent(_slimeFactory);
        builder.RegisterComponent(_turtleFactory);

        // Pool
        builder.RegisterComponent(_slimePool);
        builder.RegisterComponent(_turtlePool);
        builder.RegisterComponent(_damageTextPool);

        // Wave 
        builder.Register<WaveRunner>(Lifetime.Singleton)
               .As<IStartable>();

        // Config
        builder.RegisterInstance(_slimeConfig);
        builder.RegisterInstance(_turtleConfig);
        builder.RegisterInstance(_waveConfig);

        builder.RegisterComponent(_cameraFollow).As<ITickable>().AsSelf();
    }
}
