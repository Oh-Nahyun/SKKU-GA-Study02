using UnityEngine;

public class ItemHealthIncrease : Item
{
    [SerializeField] private int _healthIncrease = 5;

    protected override void Effect(Player player)
    {
        player._health += _healthIncrease;
    }
}