using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform[] _targetsPoint;
    [SerializeField] private NavMeshAgent _meshAgent;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private MultiAimConstraint _multiAim;
    public Transform FirstPoint => _targetsPoint[0];
    public Transform LastPoint => _targetsPoint[_targetsPoint.Length - 1];
    private int _currentTarget;
    private void Awake()
    {
        _playerManager.Move += Move;
    }

    private void Update()
    {
        if (_animator.GetBool("RUN"))
        {
            _meshAgent.SetDestination(_targetsPoint[_currentTarget].position);
        }
        if (Vector3.Distance(transform.position, _targetsPoint[_currentTarget].position) < 1f)
        {
            _meshAgent.isStopped = true;
            _animator.SetBool("RUN", false);
            _multiAim.weight = 1;
        }
    }

    private void Move()
    {
        _animator.SetBool("RUN", true);
        _multiAim.weight = 0;
        SelectTarget();
        _meshAgent.isStopped = false;
    }

    private void SelectTarget()
    {
        if (_currentTarget < _targetsPoint.Length - 1)
            _currentTarget++;
        else
            _currentTarget = _targetsPoint.Length - 1;
    }
}
