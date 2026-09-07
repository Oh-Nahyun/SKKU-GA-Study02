using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private int _damage = 100;
    [SerializeField] protected float _moveSpeed = 1f;
    [SerializeField] private Item[] _itemPrefabs = new Item[3];

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
            Destroy(gameObject); // 너죽자! // collision.gameObject
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        // Todo : Scriptable Object를 사용해서 리팩토링
        if (Random.Range(0f, 1f) > 0.3f) return;

        Instantiate(_itemPrefabs[Random.Range(0, _itemPrefabs.Length)], transform.position, transform.rotation);
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