using System;
using UnityEngine;

public class EnemyGoToPlayer : Enemy
{
    private GameObject _player;
    private Vector2 _direction;

    private void Start()
    {
        _direction = _player.transform.position - transform.position;
        _direction.Normalize();
    }

    protected override void Move()
    {
        _player = GameObject.FindWithTag("Player");
        transform.Translate(_direction * _moveSpeed * Time.deltaTime);
    }
}