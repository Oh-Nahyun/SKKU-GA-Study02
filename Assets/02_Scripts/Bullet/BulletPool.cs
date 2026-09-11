using System;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    // 싱글톤 : static(정적)
    private static BulletPool _instance = null;
    public static BulletPool Instance => _instance;

    //  오브젝트 풀링 : Pool(웅덩이, 창고)
    // 그 창고 안에 게임 오브젝트를 미리 필요한 만큼 만들어두고,
    // 필요할 때마다 꺼내서 사용하고 필요가 없으면 반환하는 식으로 (활성화/비활성화)
    // 메모리 할당과(객체의 생성) 해제(파괴)를 최소화해서 성능 UP!

    // 필요 속성
    [Header("총알 프리팹들")]
    [SerializeField] private Bullet[] _bulletPrefabs;

    [Header("풀 사이즈")]
    [SerializeField] private int _bulletPoolSize;

    // 생성한 총알을 담아둘 풀
    private Bullet[,] _bulletPool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // 1. 창고를 창고 크기만큼 만든다.
        _bulletPool = new Bullet[_bulletPrefabs.Length, _bulletPoolSize];

        // 2. 총알 프리팹 종류와 창고 크기만큼 총알을 미리 만들어서 넣는다.
        for (int i = 0; i < _bulletPrefabs.Length; i++) // [메인 총알 프리팹, 서브 총알 프리팹]
        {
            Bullet bulletPrefab = _bulletPrefabs[i];

            for (int j = 0; j < _bulletPoolSize; j++)
            {
                Bullet bullet = Instantiate(bulletPrefab, gameObject.transform);
                bullet.gameObject.SetActive(false); // 3. 당장 사용하지 않기 때문에 비활성화 한다.
                _bulletPool[i, j] = bullet;
            }
        }
    }

    public Bullet GetBullet(BulletType bulletType)
    {
        for (int i = 0; i < _bulletPool.Length; i++) // 타입별로 순회 하면서...
        {
            if (_bulletPool[i, 0].Type != bulletType) // 첫번째 요소의 타입이 내가 원하는게 아니라면 스킵
            {
                continue;
            }

            for (int j = 0; j < _bulletPoolSize; j++) // 원하는 타입의 배열 순회
            {
                Bullet bullet = _bulletPool[i, j];

                // 비활성화 되어있는 (즉, 누가 빌려가지 않은 총알 반환)
                if (bullet.gameObject.activeSelf == false)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.OnSpawn();
                    return bullet;
                }
            }
        }

        return null;
    }
}