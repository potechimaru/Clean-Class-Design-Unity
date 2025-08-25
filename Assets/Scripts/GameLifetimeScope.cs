using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameStateMachine>(Lifetime.Singleton);
        builder.Register<GameOpeningState>(Lifetime.Singleton);
        builder.Register<PlayGameState>(Lifetime.Singleton);
        builder.Register<SuccessState>(Lifetime.Singleton);
        builder.Register<FailedState>(Lifetime.Singleton);
    }
}
