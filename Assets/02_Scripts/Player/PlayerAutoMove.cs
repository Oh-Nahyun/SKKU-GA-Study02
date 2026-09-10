using System;
using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    private float _speed = 7f;

    private GameObject[] _enemies;
    private GameObject _closestEnemy;

    private void Update()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        FindClosestEnemy();

        if (_closestEnemy == null)
        {
            //Debug.Log("[AutoMode] 가장 가까운 적을 찾지 못했습니다.");
            return;
        }

        AutoMove(_closestEnemy);
    }

    private void AutoMove(GameObject enemy)
    {
        if (enemy == null) return;

        Vector2 direction = enemy.transform.position - transform.position;
        direction.Normalize();
        direction.y = 0f;
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    private void FindClosestEnemy()
    {
        if (_enemies == null || _enemies.Length == 0) return;
        _closestEnemy = _enemies[0];

        float minDistance = Vector3.Distance(transform.position, _closestEnemy.transform.position);
        minDistance = Mathf.Abs(minDistance);

        foreach (var enemy in _enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            distance = Mathf.Abs(distance);

            if (minDistance >= distance)
            {
                _closestEnemy = enemy;
                minDistance = distance;
            }
        }
    }
}