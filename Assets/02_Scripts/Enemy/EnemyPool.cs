using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private static EnemyPool _instance = null;
    public static EnemyPool Instance => _instance;

    [Header("적 프리팹들")]
    [SerializeField] private Enemy[] _enemyPrefabs;

    [Header("적 풀 사이즈")]
    [SerializeField] private int _enemyPoolSize;

    private Enemy[,] _enemyPool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        _enemyPool = new Enemy[_enemyPrefabs.Length, _enemyPoolSize];
        for (int i = 0; i < _enemyPrefabs.Length; i++)
        {
            Enemy enemyPrefab = _enemyPrefabs[i];
            for (int j = 0; j < _enemyPoolSize; j++)
            {
                Enemy enemy = Instantiate(enemyPrefab, gameObject.transform);
                enemy.gameObject.SetActive(false);
                _enemyPool[i, j] = enemy;
            }
        }
    }

    public Enemy GetEnemy(EnemyType enemyType)
    {
        for (int i = 0; i < _enemyPool.Length; i++)
        {
            if (_enemyPool[i, 0].Type != enemyType)
            {
                continue;
            }

            for (int j = 0; j < _enemyPoolSize; j++)
            {
                Enemy enemy = _enemyPool[i, j];

                if (enemy.gameObject.activeSelf == false)
                {
                    enemy.gameObject.SetActive(true);
                    return enemy;
                }
            }
        }

        return null;
    }
}
