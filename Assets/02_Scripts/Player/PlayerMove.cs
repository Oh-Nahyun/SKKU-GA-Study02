using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.

    private Animator _animator;

    private float _speed = 7f;
    public float Speed => _speed;

    public float MaxPositionY;
    public float MinPositionY;
    public float MaxPositionX;
    public float MinPositionX;

    // 객체가 생성될 때 한 번 실행된다.
    private void Awake()
    {
        // 애니메이터 컴포넌트에 대한 참조를 가져와서 할당한다.
        //_animator = this.gameObject.GetComponent<Animator>();
        _animator = GetComponent<Animator>();
        //_animator.Play("idle"); // 애니메이션을 처음부터 자꾸 실행
    }

    // 매 프레임마다 실행된다.
    // 초당 프레임 실행 횟수는 별다른 설정이 없을 경우, 가능한 많이 실행된다.
    private void Update()
    {
        Move();
        ChangeSpeed();
    }

    private void Move()
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

        // normalized : 벡터의 길이를 1로 만들어주는 것 (즉, 방향만 유지한다.)
        Vector2 normalizedDirection = new Vector2(h, v).normalized;
        _animator.SetInteger("x", (int)normalizedDirection.x);

        Vector2 newPosition = transform.position + (Vector3)(normalizedDirection * _speed) * Time.deltaTime;
        //Debug.Log($"h:{h}, v:{v}");

        // [실습 1] 이미지와 같이 빨간색 영역 안에서만 캐릭터가 이동할 수 있게 구현
        if (newPosition.y > MaxPositionY)
        {
            newPosition.y = MaxPositionY;
        }
        else if (newPosition.y < MinPositionY)
        {
            newPosition.y = MinPositionY;
        }

        // [실습 2] 좌우 이동에 있어 한쪽으로 쭈욱 이동하면 반대쪽에서 나오게 구현
        if (newPosition.x > MaxPositionX)
        {
            newPosition.x = MinPositionX;
        }
        else if (newPosition.x < MinPositionX)
        {
            newPosition.x = MaxPositionX;
        }

        // transform.Translate(normalizedSpeed * Time.deltaTime);
        transform.position = newPosition; // 새로운 위치 = 현재 위치 + 거리(방향 * 속력 * 시간)
    }

    private void ChangeSpeed()
    {
        // [실습 3] 스피드 증가/감소 기능 구현
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 키보드 E키를 누르면 스피드 Up!
            _speed++;
            Debug.Log($"Speed 증가 : {Speed}");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // 키보드 Q키를 누르면 스피드 Down!
            _speed--;
            Debug.Log($"Speed 감소 : {Speed}");
        }
    }

    public void IncreaseMoveSpeed(int moveSpeedIncrease)
    {
        _speed += moveSpeedIncrease;
    }
}