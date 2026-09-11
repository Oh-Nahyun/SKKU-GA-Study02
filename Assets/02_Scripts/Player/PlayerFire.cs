using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표 : 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알 프리팹
    public GameObject BulletFrontPrefab;
    public GameObject BulletBackPrefab;

    // - 생성 위치(총구)
    // public Transform[] ...;
    public Transform FirePointFrontLeft;
    public Transform FirePointFrontRight;
    public Transform FirePointBackLeft;
    public Transform FirePointBackRight;

    // - 쿨타임
    private float _coolTime = 0.5f;
    public float CoolTime => _coolTime;

    private float _lastTime;

    // - 발사 여부
    private bool _isFired;

    // - 자동 모드 여부
    public bool _isAutoMode;

    private void Start()
    {
        _lastTime = CoolTime;
    }

    private void Update()
    {
        ChangeCoolTime();
        Fire();
        ChangeMode();
    }

    private void Fire()
    {
        // 1. 스페이스바를 누른다.
        if (!_isFired && ((!_isAutoMode && Input.GetKeyDown(KeyCode.Space)) || _isAutoMode))
        {
            // TODO : 직접 생성이 아니라 총알 창고에서 총알 주라고 하기 -> 완료

            // 2. 총알 프리팹을 생성한다.
            // Instantiate는 프리팹을 복사해서 (MonoBehaviour를 상속받는) 게임 오브젝트를 생성하고 씬에 넣어주는 기능
            //GameObject bulletFrontLeft = Instantiate(BulletFrontPrefab);
            Bullet bulletFrontLeft = BulletPool.Instance.GetBullet(BulletType.Main);
            bulletFrontLeft.transform.position = FirePointFrontLeft.position; // 생성한 총알의 위치를 총구의 위치로 이동

            Bullet bulletFrontRight = BulletPool.Instance.GetBullet(BulletType.Main);
            bulletFrontRight.transform.position = FirePointFrontRight.position;

            Bullet bulletBackLeft = BulletPool.Instance.GetBullet(BulletType.Sub);
            bulletBackLeft.transform.position = FirePointBackLeft.position;

            Bullet bulletBackRight = BulletPool.Instance.GetBullet(BulletType.Sub);
            bulletBackRight.transform.position = FirePointBackRight.position;

            _isFired = true;
        }
    }

    private void ChangeMode()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) // Input.GetKeyDown(KeyCode.Alpha1) 
        {
            _isAutoMode = (_isAutoMode) ? false : true;
        }
    }

    private void ChangeCoolTime()
    {
        if (_isFired)
        {
            _lastTime -= Time.deltaTime;
            // Debug.Log($"남은 쿨타임 시간 : {_lastTime}");

            if (_lastTime <= 0)
            {
                _lastTime = CoolTime;
                _isFired = false;
                // Debug.Log("총알 발사 가능!");
            }
        }
    }

    public void IncreaseFireSpeed(float fireSpeedIncrease)
    {
        _coolTime -= fireSpeedIncrease;
    }
}