using Assets._Project.Develop.Runtime.Gameplay.Level;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.UserInput;

public class GameplayPresentersFactory
{
    private readonly DIContainer _container;

    public GameplayPresentersFactory(DIContainer container)
    {
        _container = container;
    }

    public GameplayScreenPresenter CreateGameplayScreen(GameplayScreenView view)
    {
        return new GameplayScreenPresenter(
            view,
            _container.Resolve<UserInputService>(),
            _container.Resolve<GameplayCycle>()
        );
    }
}