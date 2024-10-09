using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private Transform _target;

    public event UnityAction<Enemy> Released;

    private void Update()
    {
        if (_target != null)
        {
            transform.LookAt(_target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DeadEnd wall))
        {            
            Released?.Invoke(this);
        }
    }

    public void Init(Transform target)
    {
        _target = target;
    }
}
