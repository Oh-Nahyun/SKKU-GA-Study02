using UnityEngine;

public class ItemMoveSpeedIncrease : Item
{
    [SerializeField] private int _moveSpeedIncrease = 10;
    [SerializeField] private GameObject _getMoveSpeedEffectPrefab;

    protected override void Effect(Player player)
    {
        Debug.Log($"플레이어 이동 속도 증가 전 : {player._playerMove.Speed}");
        Instantiate(_getMoveSpeedEffectPrefab, transform.position, Quaternion.identity);
        player._playerMove.IncreaseMoveSpeed(_moveSpeedIncrease);
        Debug.Log($"플레이어 이동 속도 증가 후 : {player._playerMove.Speed}");
    }
}