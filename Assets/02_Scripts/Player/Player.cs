using System;
using UnityEngine;
using Random = System.Random;

public class Player : MonoBehaviour
{
    // 캡슐화
    // - 데이터 은닉
    // - 메서드를 통한 상태 변경
    [SerializeField] private int _health = 100;
    private int _maxHealth = 100;

    // 잘 설계된 클래스는
    // - 필드 (인스턴스 변수)
    // - 필드에 잘못된 값이 할당되지 않게 먹고, 정상적으로 동작하는 메서드

    // getter/setter : 특정 데이터를 get/set 해주는 메서드
    // public int GetHealth()
    // {
    //     return _health;
    // }
    // public void SetHealth(int value) // TakeDamage(), Heal()이 있기 때문에 필요하지 않는다.
    // {
    //     // 무결성 검사를 해야한다.
    //     // 무결성 : 잘못된 데이터가 들어가지 않게 하는 것
    //     // - 최대 체력보다 체력은 적어야 한다...
    //     _health = value;
    // }

    // public int Health // 프로퍼티
    // {
    //     get { return _health; }
    // }

    // 람다식 문법을 활용한 읽기 전용 프로퍼티
    public int Health => _health;

    public PlayerMove _playerMove;
    public PlayerFire _playerFire;
    [SerializeField] private GameObject[] _hitEffectPrefabs;
    [SerializeField] private GameObject _deathEffectPrefab;

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerFire = GetComponent<PlayerFire>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        int index = UnityEngine.Random.Range(0, _hitEffectPrefabs.Length);
        Instantiate(_hitEffectPrefabs[index], transform.position, Quaternion.identity);

        if (Health <= 0)
        {
            Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Heal(int healthIncrease)
    {
        _health += healthIncrease;
        if (Health >= _maxHealth) _health = _maxHealth;
    }
}