using System;
using System.Collections.Generic;

public class ControllersUpdateService
{
    private List<ConrollerToRemoveReason> _controllers = new();

    public void Add(Controller controller, Func<bool> removeReason)
    {
        _controllers.Add(new ConrollerToRemoveReason(controller, removeReason));
    }

    public void Update(float deltatime)
    {
        _controllers.RemoveAll(item => item.RemoveReason.Invoke());

        foreach (ConrollerToRemoveReason item in _controllers)
            item.Controller.Update(deltatime);
    }

    private class ConrollerToRemoveReason
    {
        public ConrollerToRemoveReason(Controller controller, Func<bool> removeReason)
        {
            Controller = controller;
            RemoveReason = removeReason;
        }

        public Controller Controller { get; }
        public Func<bool> RemoveReason { get; }
    }
}