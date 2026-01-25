using UnityEngine;

public class DoubleJumpCheck : MonoBehaviour
{
    public bool CanJump = false;

    void Update()
    {        
        CanJump = IsGrounded();
    }   
    
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);       
    }
}