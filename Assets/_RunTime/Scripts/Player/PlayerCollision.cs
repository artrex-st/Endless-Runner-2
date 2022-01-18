using UnityEngine;
public class PlayerCollision : MonoBehaviour
{
    public delegate void TriggerHitHandler(Collider other);
    public event TriggerHitHandler OnPlayerColliderHited;
    
    private void OnTriggerEnter(Collider other)
    {
        _InvokeEvent(other);
    }
    private void _InvokeEvent(Collider other)
    {
        OnPlayerColliderHited?.Invoke(other);
    }
}
