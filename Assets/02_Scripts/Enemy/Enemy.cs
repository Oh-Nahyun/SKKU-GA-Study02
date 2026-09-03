using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float _health = 100;
    public float _moveSpeed = 1f;

    private void Update()
    {
        Move();
    }

    protected abstract void Move();
}