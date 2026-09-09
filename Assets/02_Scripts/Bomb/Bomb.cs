using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator _animator;

    public int Damage = 100;

    private static readonly int IsExplosionHash = Animator.StringToHash("IsExplosion");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        _animator.SetBool(IsExplosionHash, true);
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        enemy.TakeDamage(Damage);
    }
}