using UnityEngine;

public class ItemFireSpeedIncrease : Item
{
    [SerializeField] private float _fireSpeedIncrease = 0.2f;

    protected override void Effect(Player player)
    {
        player._playerFire.CoolTime -= _fireSpeedIncrease;
    }
}