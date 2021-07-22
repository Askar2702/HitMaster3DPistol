using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _health;
    [SerializeField] private Slider _slider;
    private EnemyData _enemyData;
    private Camera _camera;
    private Rigidbody[] _rigidbodies;
    private void Awake()
    {
        _camera = Camera.main;
        _enemyData = FindObjectOfType<EnemyData>();
        _slider.maxValue = _health;
        _slider.value = _health;
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
    }
    private void Start()
    {
        _enemyData.Add(this);
        SetKinematicRigidBody(true);
    }
    public void TakeDamage(float amount)
    {
        _health -= amount;
        _slider.value = _health;
        SetKinematicRigidBody(false);
        if (_health <= 0)
        {
            _enemyData.Remove(this);
            _slider.gameObject.SetActive(false);
        }
        else StartCoroutine(Rise());
    }

    private void LateUpdate()
    {
        _canvas.transform.LookAt(_canvas.transform.position + _camera.transform.forward);
    }

    private void SetKinematicRigidBody(bool activ)
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
            _rigidbodies[i].isKinematic = activ;
    }
    IEnumerator Rise()
    {
        yield return new WaitForSeconds(2f);
        _animator.enabled = true;
        SetKinematicRigidBody(true);
    }
}
