using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int _health = 100;
    [SerializeField] public PlayerMove _playerMove;
    [SerializeField] public PlayerFire _playerFire;

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerFire = GetComponent<PlayerFire>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}