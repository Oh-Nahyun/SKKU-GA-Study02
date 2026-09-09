using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    public Animator _animator;

    // ToDo : 적이 공격 당할 때 재생시켜주는 피격 사운드
    private AudioSource _damagedAudioSource;

    [SerializeField] private int _health = 100;
    [SerializeField] private int _damage = 100;
    [SerializeField] protected float _moveSpeed = 1f;
    [SerializeField] private Item[] _itemPrefabs = new Item[3];
    [SerializeField] private GameObject _deathEffectPrefab;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damagedAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _damagedAudioSource.Play();

        if (_health <= 0)
        {
            Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); // 너죽자! // collision.gameObject
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        // ToDO : Scriptable Object를 사용해서 리팩토링
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