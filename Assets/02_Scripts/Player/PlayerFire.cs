using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표 : 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알 프리팹
    public GameObject BulletPrefab;
    // - 생성 위치(총구)
    public Transform FirePoint1;
    public Transform FirePoint2;
    
    private void Update()
    {
        // 1. 스페이스바를 누른다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 2. 총알 프리팹을 생성한다.
            // Instantiate는 프리팹을 복사해서 (MonoBehaviour를 상속받는) 게임 오브젝트를 생성하고 씬에 넣어주는 기능
            GameObject bullet1 = Instantiate(BulletPrefab);
            bullet1.transform.position = FirePoint1.position; // 생성한 총알의 위치를 총구의 위치로 이동
            
            GameObject bullet2 = Instantiate(BulletPrefab);
            bullet2.transform.position = FirePoint2.position;
        }
    }
}
