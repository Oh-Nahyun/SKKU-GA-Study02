using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private int _damage = 100;
    [SerializeField] protected float _moveSpeed = 1f;
    [SerializeField] private Item[] _itemPrefabs;

    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
            Destroy(gameObject); // 너죽자! // collision.gameObject
        }
    }

    private void Die()
    {
        int itemPrefabIndex = 0;
        float percent = Random.Range(0f, 1f);

        if (percent >= 0.7f)
        {
            itemPrefabIndex = 0;
        }
        else if (percent >= 0.4f)
        {
            itemPrefabIndex = 1;
        }
        else
        {
            itemPrefabIndex = 2;
        }

        Item item = Instantiate(_itemPrefabs[itemPrefabIndex]);
        item.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();
        if (player == null)
        {
            Debug.LogWarning("플레이어가 NULL 입니다.");
            return;
        }

        player.TakeDamage(_damage);
        Destroy(gameObject);
    }
}