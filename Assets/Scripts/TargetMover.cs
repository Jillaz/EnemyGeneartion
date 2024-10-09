using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    [SerializeField] private float _speed = 2.0f;
    private int _selectedTarget = 0;

    private void Awake()
    {
        if (_waypoints.Count != 0)
        {
            SelectTarget();
        }
    }

    private void Update()
    {
        if (_waypoints.Count != 0)
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.forward);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _waypoints[_selectedTarget])
        {
            _selectedTarget = ++_selectedTarget % _waypoints.Count;            
            SelectTarget();
        }
    }

    private void SelectTarget()
    {
        transform.LookAt(_waypoints[_selectedTarget]);
    }
}
