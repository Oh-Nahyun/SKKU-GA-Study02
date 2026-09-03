using UnityEngine;

public class EnemyFollowPlayer : Enemy
{
    public EnemyFollowPlayer(float health, float moveSpeed)
    {
        _health = health;
        _moveSpeed = moveSpeed;
    }

    protected override void Move()
    {
        GameObject player = GameObject.Find("Player");
        Vector2 direction = player.transform.position - transform.position;
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }
}