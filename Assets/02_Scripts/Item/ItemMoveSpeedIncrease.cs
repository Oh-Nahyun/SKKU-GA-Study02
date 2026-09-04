using UnityEngine;

public class ItemMoveSpeedIncrease : Item
{
    [SerializeField] private int _moveSpeedIncrease = 10;

    protected override void Effect(Player player)
    {
        Debug.Log($"플레이어 이동 속도 증가 전 : {player._playerMove.Speed}");
        player._playerMove.Speed += _moveSpeedIncrease;
        Debug.Log($"플레이어 이동 속도 증가 후 : {player._playerMove.Speed}");
    }
}