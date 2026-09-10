using System;
using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;

    private GameObject[] _enemies;
    private GameObject _closestEnemy;

    private void Update()
    {
        FindClosestEnemy();
        AutoMove();
    }

    private void AutoMove()
    {
        if (_closestEnemy == null)
        {
            //Debug.Log("[AutoMode] 가장 가까운 적을 찾지 못했습니다.");
            return;
        }

        Vector2 direction = _closestEnemy.transform.position - transform.position;
        direction.y = 0f;
        direction.Normalize();
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    private void FindClosestEnemy()
    {
        if (_closestEnemy != null) return;

        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (_enemies == null || _enemies.Length == 0) return;

        float minDistance = float.MaxValue;
        _closestEnemy = _enemies[0];

        foreach (var enemy in _enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            distance = Mathf.Abs(distance);

            if (minDistance > distance)
            {
                minDistance = distance;
                _closestEnemy = enemy;
            }
        }
    }
}