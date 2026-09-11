using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    private Player _player;
    public GameObject BombPrefab;
    private GameObject _bombGameObject;

    [SerializeField] private float _duration = 3f;
    [SerializeField] private float _coolTime = 10f;
    public float CoolTime => _coolTime;
    private float _countTime;

    private bool _isUsed;
    private bool _isExplosionEnd;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _countTime = 0f;
    }

    private void Update()
    {
        ChangeCoolTime();
        PlantBomb();
    }

    private void PlantBomb()
    {
        if (_isUsed || !Input.GetKeyDown(KeyCode.B)) return;

        float x = Random.Range(_player._playerMove.MinPositionX, _player._playerMove.MaxPositionX
        );
        float y = Random.Range(_player._playerMove.MaxPositionY, _player._playerMove.MinPositionY
        ) * (-1);

        _bombGameObject = Instantiate(BombPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        _isExplosionEnd = false;
        _isUsed = true;
    }

    private void ChangeCoolTime()
    {
        if (!_isUsed) return;

        _countTime += Time.deltaTime;

        if (_countTime >= _duration && !_isExplosionEnd)
        {
            //Debug.Log("---폭탄제거---");
            Bomb bomb = _bombGameObject.GetComponent<Bomb>();
            bomb.ChangeIsExplosion(false);
            Destroy(_bombGameObject, 0.13f);
            _isExplosionEnd = true;
        }

        if (_countTime >= CoolTime)
        {
            _countTime = 0f;
            _isExplosionEnd = false;
            _isUsed = false;
        }
    }
}