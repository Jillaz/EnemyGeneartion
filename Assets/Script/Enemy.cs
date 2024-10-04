using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public event UnityAction<Enemy> Release;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Wall wall))
        {
            Release?.Invoke(this);
        }
    }
}
