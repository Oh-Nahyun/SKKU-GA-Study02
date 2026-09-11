using System;
using UnityEngine;
using Random = UnityEngine.Random;

// 역할 : 일정 시간마다 적을 생성해주고 싶다.

public class EnemySpawner : MonoBehaviour
{
    // 필요 속성
    [SerializeField] private EnemySpawnDataTableSO _spawnDataTable;

    // - 타이머
    //[Header("스폰간격")][SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private float _spawnInterval = 3f;
    private float _timer;

    // - 생성할 프리팹들
    //[Header("스폰할 적 프리팹")][SerializeField] private GameObject _enemyPrefab;
    //[Header("스폰할 적 프리팹")][SerializeField] private Enemy _enemyPrefab; // Enemy 클래스까지 가지고 있다.
    //[SerializeField] private Enemy[] _enemyPrefabs = new Enemy[3];

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
        // ToDO : Scriptable Object를 사용해서 리팩토링
        // 이유 1 : 배열을 사용했지만 각 아이템이 어떤 프리팹인지 알 수 없음
        // 이유 2 : 각 적 스폰 확률을 매직 넘버로 하드코딩해서 유지보수가 어려움

        // [1] 리팩토링 전 확률 코드 (퍼센트 기반)
        //
        // int enemyPrefabIndex = 0;
        // float percent = Random.Range(0f, 1f);
        //
        // if (percent >= 0.5f)
        // {
        //     // 50% : [0] Downward
        //     enemyPrefabIndex = 0;
        // }
        // else if (percent >= 0.2f)
        // {
        //     // 30% : [1] Aimed
        //     enemyPrefabIndex = 1;
        // }
        // else
        // {
        //     // 20% : [2] Homing
        //     enemyPrefabIndex = 2;
        // }
        //
        // Enemy enemy = Instantiate(_enemyPrefabs[enemyPrefabIndex]);
        // enemy.transform.position = transform.position;

        // [2] Scriptable Object를 사용한 가중치 랜덤 선택 코드
        // 각 아이템에 가중치를 부여하고, 가중치가 클수록 높은 확률로 선택되도록 하는 방식
        // 1. 추첨할 수 있는 모든 가중치를 더한다.
        int totalWeight = 0;
        foreach (EnemySpawnData data in _spawnDataTable.Datas)
        {
            totalWeight += data.Weight;
        }

        // 2. 전체 가중치 범위에서 랜덤한 정수를 뽑는다.
        int randomWeight = Random.Range(0, totalWeight);

        // 3. 가중치를 누적하면서 선택된 구간을 찾는다.
        int cumulativeWeight = 0;
        foreach (EnemySpawnData data in _spawnDataTable.Datas)
        {
            cumulativeWeight += data.Weight; // 누적
            if (randomWeight < cumulativeWeight) // 구간
            {
                GameObject enemy = Instantiate(data.EnemyPrefab);
                enemy.transform.position = transform.position;
                break;
            }
        }
    }
}