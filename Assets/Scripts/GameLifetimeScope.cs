using State.EnemyState;
using State.GameState;
using State.PlayerState;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerView _playerView;

    [SerializeField] private CoinFactory _coinFactory;
    [SerializeField] private SlashEffectFactory _slashEffectFactory;

    [SerializeField] private SlimePool _slimePool;
    [SerializeField] private TurtlePool _turtlePool;
    [SerializeField] private DamageTextPool _damageTextPool;
    [SerializeField] private CoinPool _coinPool;
    [SerializeField] private SlashEffectPool _slashEffectPool;

    [SerializeField] private SlimeConfig _slimeConfig;
    [SerializeField] private TurtleConfig _turtleConfig;
    [SerializeField] private WaveConfig _waveConfig;
    [SerializeField] private UpgradeCostConfig _upgradeCostConfig;

    [SerializeField] private CameraFollow _cameraFollow;

    [SerializeField] private Chicken _chicken;

    [SerializeField] private AttackUpgradeButton _attackUpgradeButton;
    [SerializeField] private ShieldUpgradeButton _shieldUpgradeButton;
    [SerializeField] private BeamUpgradeButton _beamUpgradeButton;
    [SerializeField] private HPUpgradeButton _hpUpgradeButton;
    [SerializeField] private GetLowHealButton _getLowHealButton;
    [SerializeField] private GetHighHealButton _getHighHealButton;
    [SerializeField] private GetFullHealButton _getFullHealButton;
    [SerializeField] private GetRemedyButton _getRemedyButton;
    [SerializeField] private GetPowerBuffButton _getPowerBuffButton;
    [SerializeField] private GetSpeedBuffButton _getSpeedBuffButton;


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
        builder.Register<PlayerController>(Lifetime.Singleton).As<ITickable>().AsSelf(); 
        builder.Register<InputService>(Lifetime.Singleton);

        builder.Register<PlayerFacade>(Lifetime.Singleton);

        // Factory
        builder.Register<SlimeFactory>(Lifetime.Singleton).As<IInitializable>().AsSelf();
        builder.Register<TurtleFactory>(Lifetime.Singleton).As<IInitializable>().AsSelf();
        builder.RegisterComponent(_coinFactory);
        builder.RegisterComponent(_slashEffectFactory);

        // EnemyFactoryRegistry“o˜^
        builder.Register<EnemyFactoryRegistry>(resolver =>
        {
            var slimeFactory = resolver.Resolve<SlimeFactory>();
            var turtleFactory = resolver.Resolve<TurtleFactory>();

            var dict = new Dictionary<EnemyType, IEnemyFactory>
        {
            { EnemyType.Slime, slimeFactory },
            { EnemyType.Turtle, turtleFactory },
        };

            return new EnemyFactoryRegistry(dict);
        }, Lifetime.Singleton);

        // Pool
        builder.RegisterComponent(_slimePool);
        builder.RegisterComponent(_turtlePool);
        builder.RegisterComponent(_damageTextPool);
        builder.RegisterComponent(_coinPool);
        builder.RegisterComponent(_slashEffectPool);

        // Wave 
        builder.Register<WaveRunner>(Lifetime.Singleton)
               .As<IStartable>();

        // Config
        builder.RegisterInstance(_slimeConfig);
        builder.RegisterInstance(_turtleConfig);
        builder.RegisterInstance(_waveConfig).As<IWaveConfig>();
        builder.RegisterInstance(_upgradeCostConfig);
        builder.Register<EnemyConfigFacade>(Lifetime.Singleton).As<IEnemyConfigFacade>();

        builder.RegisterComponent(_cameraFollow).As<ITickable>().AsSelf();

        builder.RegisterComponent(_chicken);

        builder .Register<UpgradeManager>(Lifetime.Singleton).AsSelf();
        builder.Register<GetItemManager>(Lifetime.Singleton).AsSelf();
        
        builder.RegisterComponent(_attackUpgradeButton);
        builder.RegisterComponent(_shieldUpgradeButton);
        builder.RegisterComponent(_hpUpgradeButton);
        builder.RegisterComponent(_beamUpgradeButton);

        builder.RegisterComponent(_getLowHealButton);
        builder.RegisterComponent(_getHighHealButton);
        builder.RegisterComponent(_getFullHealButton);
        builder.RegisterComponent(_getRemedyButton);
        builder.RegisterComponent(_getPowerBuffButton);
        builder.RegisterComponent(_getSpeedBuffButton);

    }
}
