using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.

    public float Speed = 0.05f;
    
    // 매 프레임마다 실행된다.
    // 초당 프레임 실행 횟수는 별다른 설정이 없을 경우, 가능한 많이 실행된다.
    private void Update()
    {
        // // 1. 키보드 입력을 받는다.
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     Debug.Log("왼쪽 방향키를 누르는 중");
        //
        //     // 2. 키보드 입력에 따라 방향을 구한다.
        //     // - 게임에는 벡터라는 타입이 있다. 벡터는 크기와 방향을 의미한다.
        //     Vector2 direction = new Vector2(-1, 0); // 왼쪽 방향 = Vector2 direction = Vector2.left;
        //
        //     // 3. 방향과 속력에 따라 이동한다.
        //     // - 속도 = 방향 * 속력
        //     // - 매직 넘버 : 보는 사람에 따라 의미가 달라질 수 있는 헷갈리는 숫자 (0.05f)
        //     //              transform.Translate(direction * 0.05f);
        //     // - deltaTime : 이전 프레임으로부터 지금 프레임까지 시간이 얼마나 지났는지 ms(밀리세컨드) 단위로 반환
        //     transform.Translate(direction * Speed * Time.deltaTime);
        // }
        
        // ---
        
        // GetAxis : ("Horizontal") 키보드 왼/오른쪽 입력 상태에 따라 -1f ~ 0 ~ 1f
        //           ("Vertical") 키보드 위/아래쪽 입력 상태에 따라 -1f ~ 0 ~ 1f
        // GetAxisRaw : -1, 0, 1 중으로 값이 정해진다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(h, v);
        //Debug.Log($"h:{h}, v:{v}");
        
        // [실습 1] 이미지와 같이 빨간색 영역 안에서만 캐릭터가 이동할 수 있게 구현
        if (direction.y <= 0 && transform.position.y <= -5.0f)
        {
            direction.y = 0;
        }
        if (direction.y >= 0 && transform.position.y >= -3.0f)
        {
            direction.y = 0;
        }

        // [실습 2] 좌우 이동에 있어 한쪽으로 쭈욱 이동하면 반대쪽에서 나오게 구현
        if (direction.x <= 0 && transform.position.x <= -2.0f)
        {
            transform.position = new Vector3(- transform.position.x, transform.position.y, 0);
        }
        if (direction.x >= 0 && transform.position.x >= 2.0f)
        {
            transform.position = new Vector3(- transform.position.x, transform.position.y, 0);
        }
        
        // normalized : 벡터의 길이를 1로 만들어주는 것 (즉, 방향만 유지한다.)
        // Vector2 normalizedSpeed = (direction * Speed).normalized;
        // transform.Translate(direction * normalizedSpeed * Time.deltaTime);
        
        // 새로운 위치 = 현재 위치 + 거리(방향 * 속력 * 시간)
        transform.position += (Vector3)direction * Speed * Time.deltaTime;
    }
}
