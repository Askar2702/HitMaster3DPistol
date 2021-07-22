using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour , IEnumerable
{
    private List<Enemy> _enemies = new List<Enemy>();
    public Enemy this[int index]
    {
        get
        {
            return _enemies[index];
        }
    }
    public int Count => _enemies.Count;

    public void Add(Enemy enemy)
    {
        _enemies.Add(enemy);
    }
    public void Remove(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
    public IEnumerator<Enemy> GetEnumerator()
    {
        return _enemies.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _enemies.GetEnumerator();
    }
}
