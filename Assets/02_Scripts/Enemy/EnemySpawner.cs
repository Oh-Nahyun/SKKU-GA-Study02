using UnityEngine;

// 역할 : 일정 시간마다 적을 생성해주고 싶다.

public class EnemySpawner : MonoBehaviour
{
    // 필요 속성
    // - 타이머
    [Header("스폰간격")][SerializeField] private float _spawnInterval = 3f;
    private float _timer;

    // - 생성할 프리팹
    //[Header("스폰할 적 프리팹")] [SerializeField] private GameObject _enemyPrefab;
    //[Header("스폰할 적 프리팹")][SerializeField] private Enemy _enemyPrefab; // Enemy 클래스까지 가지고 있다.
    [Header("스폰할 적 프리팹")][SerializeField] private Enemy[] _enemyPrefabs = new Enemy[3];

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0;
            _spawnInterval = Random.Range(1f, 3f); // float : 1 ~ 3 // UnityEngine의 Random.Range
            //int randomInt = Random.Range(1, 3); // int : 1 ~ 2
            Spawn();
        }
    }

    private void Spawn()
    {
        //Enemy enemy = Instantiate(enemyPrefab);
        //enemy.transform.position = transform.position;

        Enemy enemy;
        float percent = Random.Range(0f, 10f);

        if (percent >= 5f)
        {
            // 50%: Downward
            enemy = Instantiate(_enemyPrefabs[0]);
        }
        else if (percent >= 2f)
        {
            // 30%: Aimed
            enemy = Instantiate(_enemyPrefabs[1]);
        }
        else
        {
            // 20%: Homing
            enemy = Instantiate(_enemyPrefabs[2]);
        }

        enemy.transform.position = transform.position;
    }
}