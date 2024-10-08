using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private int _speed = 2;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.forward);
    }
}
