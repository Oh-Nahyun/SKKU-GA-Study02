using System;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표 : 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알 프리팹
    public GameObject BulletPrefab;
    // - 생성 위치(총구)
    public Transform FirePointLeft;
    public Transform FirePointRight;
    // - 쿨타임
    public float CoolTime;
    private float lastTime;
    private bool isFired = false;

    private void Start()
    {
        lastTime = CoolTime;
    }

    private void Update()
    {
        if (isFired)
        {
            lastTime -= Time.deltaTime;
            Debug.Log($"남은 쿨타임 시간 : {lastTime}");
            
            if (lastTime <= 0)
            {
                lastTime = CoolTime;
                isFired = false;
                Debug.Log("총알 발사 가능!");
            }
        }
        
        // 1. 스페이스바를 누른다.
        if (!isFired && Input.GetKeyDown(KeyCode.Space))
        {
            // 2. 총알 프리팹을 생성한다.
            // Instantiate는 프리팹을 복사해서 (MonoBehaviour를 상속받는) 게임 오브젝트를 생성하고 씬에 넣어주는 기능
            GameObject bulletLeft = Instantiate(BulletPrefab);
            bulletLeft.transform.position = FirePointLeft.position; // 생성한 총알의 위치를 총구의 위치로 이동
            
            GameObject bulletRight = Instantiate(BulletPrefab);
            bulletRight.transform.position = FirePointRight.position;

            isFired = true;
            Debug.Log("총알 발사 완료!");
        }
    }
}