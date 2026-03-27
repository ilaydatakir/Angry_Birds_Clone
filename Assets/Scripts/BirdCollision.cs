using UnityEngine;

public class BirdCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.gameObject.name;

        if (name.Contains("pig") || name.Contains("Wood") || name.Contains("wood"))
        {
            CameraShake shake = Camera.main.GetComponent<CameraShake>();
            if (shake != null)
            {
                shake.Shake();
            }
        }
    }
}