using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System;

public class PlayerManager : MonoBehaviour
{
    public event Action<Transform , string> Shoot;
    public event Action Move;
    private EnemyData _enemyData;
    private bool isFire;
    private void Awake()
    {
        _enemyData = FindObjectOfType<EnemyData>();
    }

    private void Update()
    {
        RaycastHit hit;
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            FindClosestEnemy();
            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.transform.GetComponent<Rigidbody>() || !isFire) return;
                Shoot?.Invoke(hit.transform, hit.transform.name);
            }
        }
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (touch.phase == TouchPhase.Began)
            {
                FindClosestEnemy();
                if (Physics.Raycast(ray, out hit))
                {
                    if (!hit.transform.GetComponent<Rigidbody>() || !isFire) return;
                    Shoot?.Invoke(hit.transform, hit.transform.name);
                }
            }

        }
#endif
    }

    private void FindClosestEnemy()
        {
            float distanceClosestEnemy = Mathf.Infinity;
            Enemy enemy = null;
            foreach (var enem in _enemyData)
            {
                float distenemy = (enem.transform.position - transform.position).sqrMagnitude;
                if (distenemy < distanceClosestEnemy)
                {
                    distanceClosestEnemy = distenemy;
                    enemy = enem;
                }
            }
            if (_enemyData.Count == 0 || Vector3.Distance(transform.position, enemy.transform.position) > 10f)
                NextPoint();
            else isFire = true;

        }
    private void NextPoint()
        {
            Move?.Invoke();
            isFire = false;
        }


    
}

