using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    public Animator _animator;

    // ToDo : 적이 공격 당할 때 재생시켜주는 피격 사운드 -> 완료
    public AudioSource _damagedAudioSource;

    [SerializeField] private int _health = 100;
    [SerializeField] private int _damage = 100;
    [SerializeField] protected float _moveSpeed = 1f;

    [SerializeField] private ItemSpawnDataTableSO _itemSpawnDataTable;

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

        if (_health <= 0)
        {
            Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);

            // 싱글톤 패턴
            // 1. 전역적으로 접근 가능하다.
            // 2. 인스턴스(생성된 객체)가 하나임을 보장한다.
            //ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
            //scoreManager.AddScore(100);
            ScoreManager.Instance.AddScore(100);

            Destroy(gameObject); // 너죽자! // collision.gameObject
            SpawnItem();
        }
        else
        {
            _damagedAudioSource.Play();
        }
    }

    private void SpawnItem()
    {
        // ToDO : Scriptable Object를 사용해서 리팩토링 -> 완료

        // [1] 리팩토링 전 확률 코드 (퍼센트 기반)
        // if (Random.Range(0f, 1f) > 0.3f) return;
        // Instantiate(_itemPrefabs[Random.Range(0, _itemPrefabs.Length)], transform.position, transform.rotation);

        // [2] Scriptable Object를 사용한 가중치 랜덤 선택 코드
        int totalWeight = 0;
        foreach (ItemSpawnData data in _itemSpawnDataTable.Datas)
        {
            totalWeight += data.Weight;
        }

        int randomWeight = Random.Range(0, totalWeight);

        int cumulativeWeight = 0;
        foreach (ItemSpawnData data in _itemSpawnDataTable.Datas)
        {
            cumulativeWeight += data.Weight;
            if (randomWeight < cumulativeWeight)
            {
                GameObject item = Instantiate(data.ItemPrefab);
                item.transform.position = transform.position;
                break;
            }
        }
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