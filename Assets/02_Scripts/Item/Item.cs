using System;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
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