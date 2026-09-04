using UnityEngine;

public class ItemMoveSpeedIncrease : Item
{
    [SerializeField] private int _moveSpeedIncrease = 10;

    protected override void Effect(Player player)
    {
        player._playerMove.Speed += _moveSpeedIncrease;
    }
}