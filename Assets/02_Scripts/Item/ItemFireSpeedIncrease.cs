using UnityEngine;

public class ItemFireSpeedIncrease : Item
{
    [SerializeField] private float _fireSpeedIncrease = 0.2f;
    [SerializeField] private GameObject _getFireSpeedEffectPrefab;

    protected override void Effect(Player player)
    {
        Debug.Log($"플레이어 공격 속도 증가 전 : {player._playerFire.CoolTime}");
        Instantiate(_getFireSpeedEffectPrefab, transform.position, Quaternion.identity);
        player._playerFire.IncreaseFireSpeed(_fireSpeedIncrease);
        Debug.Log($"플레이어 공격 속도 증가 후 : {player._playerFire.CoolTime}");
    }
}