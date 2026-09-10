using System;
using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _differenceY = 3f;
    [SerializeField] private float _stopTrackingY = -2f;

    private GameObject[] _enemies;
    private GameObject _closestEnemy;

    private void Update()
    {
        FindClosestEnemy(); //
        AutoMove();
    }

    private void AutoMove()
    {
        if (_closestEnemy == null) return;

        Vector3 diff = _closestEnemy.transform.position - transform.position;
        Vector3 direction = diff;

        // 플레이어와 적과의 y축 차이가 3보다 크면 앞으로 이동하고 아니라면 뒤로 이동
        if (diff.y >= _differenceY)
        {
            direction.y = 1;
        }
        else
        {
            direction.y = -1;
        }

        direction.Normalize();
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    private void FindClosestEnemy()
    {
        if (_closestEnemy != null) return;
        if (_closestEnemy.transform.position.y > _stopTrackingY) return; //

        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (_enemies == null || _enemies.Length == 0) return;

        float minDistance = float.MaxValue;
        _closestEnemy = _enemies[0];

        foreach (GameObject enemy in _enemies)
        {
            if (enemy.transform.position.y < _stopTrackingY) continue;

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