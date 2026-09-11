using System;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // DestroyZone에 들어온 다른 게임 오브젝트는 누구든 파괴
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}