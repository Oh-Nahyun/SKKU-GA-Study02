using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Material _material;

    private float _offsetY = 0f;
    [SerializeField] private float _scrollSpeed = 0.1f;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        _offsetY += _scrollSpeed * Time.deltaTime;

        // ToDO: 머터리얼 프로퍼티 블록을 활용한 최적화
        _material.mainTextureOffset = new Vector2(0, _offsetY);
    }
}