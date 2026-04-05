public interface ICompositeController
{
    void Enable();
    
    void Disable();
    
    void Update(float deltaTime);

    void StopMove();
}