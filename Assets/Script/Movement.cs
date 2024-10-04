using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private int _speed = 2;

    void Update()
    {
        transform.Translate(_speed * Time.deltaTime* Vector3.forward);
    }
}
