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
            return;
        }

        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(_damage);
        }
    }
}