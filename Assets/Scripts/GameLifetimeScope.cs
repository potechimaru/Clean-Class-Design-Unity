using VContainer;
using VContainer.Unity;
using UnityEngine;
using State.GameState;
using State.PlayerState;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerView _playerView;
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
    }

}
