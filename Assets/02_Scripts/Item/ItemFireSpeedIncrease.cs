using UnityEngine;

public class ItemFireSpeedIncrease : Item
{
    [SerializeField] private float _fireSpeedIncrease = 0.2f;

    protected override void Effect(Player player)
    {
        Debug.Log($"플레이어 공격 속도 증가 전 : {player._playerFire.CoolTime}");
        player._playerFire.CoolTime -= _fireSpeedIncrease;
        Debug.Log($"플레이어 공격 속도 증가 후 : {player._playerFire.CoolTime}");
    }
}