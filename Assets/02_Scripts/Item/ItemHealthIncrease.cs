using UnityEngine;

public class ItemHealthIncrease : Item
{
    [SerializeField] private int _healthIncrease = 5;
    [SerializeField] private GameObject _getHealEffectPrefab;

    protected override void Effect(Player player)
    {
        //Debug.Log($"플레이어 체력 증가 전 : {player.Health}");
        Instantiate(_getHealEffectPrefab, transform.position, Quaternion.identity);
        player.Heal(_healthIncrease);
        //Debug.Log($"플레이어 체력 증가 후 : {player.Health}");
    }
}