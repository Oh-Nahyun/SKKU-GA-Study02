using UnityEngine;

public class ItemHealthIncrease : Item
{
    [SerializeField] private int _healthIncrease = 5;

    protected override void Effect(Player player)
    {
        Debug.Log($"플레이어 체력 증가 전 : {player._health}");
        player._health += _healthIncrease;
        if (player._health >= 100) player._health = 100;
        Debug.Log($"플레이어 체력 증가 후 : {player._health}");
    }
}