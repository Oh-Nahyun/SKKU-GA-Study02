using System;
using UnityEngine;

public class EnemyGoToPlayer : Enemy
{
    private GameObject _player;
    private Vector2 _direction;
    private float _angle;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");

        if (_player == null)
        {
            Debug.LogWarning("플레이어가 NULL 입니다.");
            return;
        }

        _direction = _player.transform.position - transform.position;
        _angle = 90 + Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        _direction.Normalize();
    }

    protected override void Move()
    {
        if (_player == null)
        {
            Debug.LogWarning("플레이어가 NULL 입니다.");
            return;
        }

        //transform.Translate(_direction * _moveSpeed * Time.deltaTime);
        transform.position += (Vector3)(_direction * _moveSpeed) * Time.deltaTime;
    }
}