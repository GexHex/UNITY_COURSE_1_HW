public abstract class Controller
{
    private bool _isEnabled;
    public virtual void Enable() => _isEnabled = true;
    public virtual void Disable() => _isEnabled = false;

    public void Update(float delaTime)
    {
        if (_isEnabled == false)
            return;

        UpdateLogic(delaTime);
    }

    protected abstract void UpdateLogic(float delaTime);
}