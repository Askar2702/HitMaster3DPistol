using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private PlayerManager _playerManager;
    private Transform _enemy;
    private float _damage = 30;
    private BulletPool _bulletPool;

    private void Awake()
    {
        _playerManager.Shoot += SetTarget;
        _bulletPool = FindObjectOfType<BulletPool>();
    }

    private void SetTarget(Transform enemy , string str)
    {
        if (str == "Head") _damage = 200f;
        else _damage = 30f;
        _enemy = enemy;
        var seq = DOTween.Sequence();
        seq.Append(_target.DOMove(new Vector3(enemy.position.x, enemy.position.y - 1f, enemy.position.z), 0.3f));
        seq.OnComplete(Shoot);
    }
    private void Shoot()
    {
        _shotPoint.LookAt(_target);
        var bullet = _bulletPool.GetBullet();
        bullet.transform.position = _shotPoint.position;
        bullet.Rb.AddForce(_shotPoint.forward * 3500, ForceMode.Force);

        var g = _enemy.GetComponentsInParent<Animator>();
        for (int i = 0; i < g.Length; i++)
            g[i].enabled = false;


        var e = _enemy.GetComponentsInParent<Enemy>();
        for (int i = 0; i < g.Length; i++)
            e[i].TakeDamage(_damage);

        _enemy.GetComponent<Rigidbody>().AddForce(_shotPoint.forward * 5000, ForceMode.Force);
        _target.DOLocalMove(new Vector3(0f, 1.6f, 1.8f), 0.3f);

    }
}
