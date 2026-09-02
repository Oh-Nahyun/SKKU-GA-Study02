using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        transform.position += (Vector3)Vector2.up * Speed * Time.deltaTime;
    }
}
