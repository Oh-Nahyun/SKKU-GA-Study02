using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int Damage = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        enemy.TakeDamage(Damage);
    }
}