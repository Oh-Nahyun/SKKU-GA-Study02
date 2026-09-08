using System;
using UnityEngine;

public class EnemyFollowPlayer : Enemy
{
    // 캐싱 : 자주 쓸법한 데이터(객체)를 가까운 곳에 저장해두고 쓰는 것
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    protected override void Move()
    {
        if (_player == null)
        {
            Debug.LogWarning("플레이어가 NULL 입니다.");
            return;
        }

        Vector2 direction = _player.transform.position - transform.position;
        float angle = 90 + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //transform.Translate(direction * _moveSpeed * Time.deltaTime);
        transform.position += (Vector3)(direction * _moveSpeed) * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        direction.Normalize();
    }
}