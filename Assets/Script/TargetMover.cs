using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    [SerializeField] private float _speed = 2.0f;
    private int _firstTarget = 0;
    private int _selectedTarget;

    private void Awake()
    {
        if (_waypoints.Count != 0)
        {
            _selectedTarget = _firstTarget;
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

    private void SelectTarget()
    {
        Transform currentTarget;

        currentTarget = _waypoints[_selectedTarget];
        transform.LookAt(currentTarget);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _waypoints[_selectedTarget])
        {
            if (_waypoints.Count == (_selectedTarget + 1))
            {
                _selectedTarget = _firstTarget;
            }
            else
            {
                _selectedTarget++;
            }

            SelectTarget();
        }
    }
}
