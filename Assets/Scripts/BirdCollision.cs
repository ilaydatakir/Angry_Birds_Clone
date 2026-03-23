using UnityEngine;

public class BirdCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.gameObject.name;

        // Sadece ahşap kutular ve domuzlar için kamera sallansın
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