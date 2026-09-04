using System;
using UnityEngine;

public class EnemyGoToPlayer : Enemy
{
    private GameObject _player;
    private Vector2 _direction;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");

        if (_player == null)
        {
            Debug.LogWarning("플레이어를 찾을 수 없어 방향을 설정하지 못했습니다.");
            return;
        }

        _direction = _player.transform.position - transform.position;
        _direction.Normalize();
    }

    protected override void Move()
    {
        if (_player == null) return;

        transform.Translate(_direction * _moveSpeed * Time.deltaTime);
    }
}