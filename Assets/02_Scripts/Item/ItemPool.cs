using System;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private static ItemPool _instance = null;
    public static ItemPool Instance => _instance;

    [Header("아이템 프리팹들")]
    [SerializeField] private Item[] _itemPrefabs;

    [Header("아이템 풀 사이즈")]
    [SerializeField] private int _itemPoolSize;

    private Item[,] _itemPool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        _itemPool = new Item[_itemPrefabs.Length, _itemPoolSize];
        for (int i = 0; i < _itemPrefabs.Length; i++)
        {
            Item itemPrefab = _itemPrefabs[i];
            for (int j = 0; j < _itemPoolSize; j++)
            {
                Item item = Instantiate(itemPrefab, gameObject.transform);
                item.gameObject.SetActive(false);
                _itemPool[i, j] = item;
            }
        }
    }

    public Item GetItem(ItemType itemType)
    {
        for (int i = 0; i < _itemPool.Length; i++)
        {
            if (_itemPool[i, 0].Type != itemType)
            {
                continue;
            }

            for (int j = 0; j < _itemPoolSize; j++)
            {
                Item item = _itemPool[i, j];

                if (item.gameObject.activeSelf == false)
                {
                    item.gameObject.SetActive(true);
                    return item;
                }
            }
        }

        return null;
    }
}