using System;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _stopTime = 1f;
    private float _timer = 0;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        StopFewSecondAndMove();
        Move();
    }

    private void StopFewSecondAndMove()
    {
        _timer += Time.deltaTime;

        if (_timer >= _stopTime)
        {
            _timer = 0;
        }
    }

    private void Move()
    {
        if (_player == null)
        {
            Debug.LogWarning("플레이어가 NULL 입니다.");
            return;
        }

        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }

    protected abstract void Effect(Player player);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            Debug.LogWarning("플레이어가 NULL 입니다.");
            return;
        }

        Player player = other.GetComponent<Player>();
        Effect(player);
        Destroy(gameObject);
    }
}