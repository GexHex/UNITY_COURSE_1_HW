using Assets._Project.Develop.Runtime.Gameplay.Level;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.UserInput;
using System.Collections.Generic;

public class GameplayScreenPresenter : IPresenter
{
    private readonly GameplayScreenView _screen;
    private readonly List<IPresenter> _childPresenters = new();
    private readonly UserInputService _userInputService;
    private readonly GameplayCycle _gameplayCycle;

    public GameplayScreenPresenter(
        GameplayScreenView screen,
        UserInputService userInputService,
        GameplayCycle gameCycle)
    {
        _screen = screen;
        _userInputService = userInputService;
        _gameplayCycle = gameCycle;
    }

    public void Initialize()
    {
        foreach (IPresenter presenter in _childPresenters)
            presenter.Initialize();

        _userInputService.Pressed += OnPressed;

        _screen.GeneratedText.text += new string(_gameplayCycle.GetRigthAnswer().ToArray());
    }

    private void OnPressed(char text)
    {
        _screen.UserAnswer.text += text.ToString();
    }

    public void Dispose()
    {
        foreach (IPresenter presenter in _childPresenters)
            presenter.Dispose();

        _userInputService.Pressed -= OnPressed;

        _childPresenters.Clear();
    }
}