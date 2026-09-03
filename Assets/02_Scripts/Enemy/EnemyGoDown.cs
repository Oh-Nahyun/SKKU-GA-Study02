using UnityEngine;

public class EnemyGoDown : Enemy
{
    public EnemyGoDown(float health, float moveSpeed)
    {
        _health = health;
        _moveSpeed = moveSpeed;
    }

    protected override void Move()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }
}