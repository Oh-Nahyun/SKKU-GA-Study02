using System;
using UnityEngine;

public class EnemyGoToPlayer : Enemy
{
    private Vector2 _direction;

    public EnemyGoToPlayer(float health, float moveSpeed)
    {
        _health = health;
        _moveSpeed = moveSpeed;
    }

    public void Start()
    {
        GameObject player = GameObject.Find("Player");
        _direction = player.transform.position - transform.position;
    }

    protected override void Move()
    {
        transform.Translate(_direction * _moveSpeed * Time.deltaTime);
    }
}