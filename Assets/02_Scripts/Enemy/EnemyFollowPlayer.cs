using System;
using UnityEngine;

public class EnemyFollowPlayer : Enemy
{
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    protected override void Move()
    {
        _player = GameObject.FindWithTag("Player");
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }
}