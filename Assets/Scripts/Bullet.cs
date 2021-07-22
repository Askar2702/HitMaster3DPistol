using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody Rb;
    private BulletPool _bulletPool;
    private void Awake()
    {
        _bulletPool = FindObjectOfType<BulletPool>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        _bulletPool.AddPool(this);
        gameObject.SetActive(false);
    }

}
