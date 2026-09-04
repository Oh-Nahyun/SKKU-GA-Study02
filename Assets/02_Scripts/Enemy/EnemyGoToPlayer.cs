using System;
using UnityEngine;

public class EnemyGoToPlayer : Enemy
{
    private GameObject _player;
    private Vector2 _direction;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");

        if (_player != null)
        {
            _direction = _player.transform.position - transform.position;
            _direction.Normalize();
        }
        else
        {
            Debug.LogWarning("플레이어를 찾을 수 없어 방향을 설정하지 못했습니다.");
        }
    }

    protected override void Move()
    {
        transform.Translate(_direction * _moveSpeed * Time.deltaTime);
    }
}