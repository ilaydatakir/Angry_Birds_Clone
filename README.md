# An Angry Birds Clone Project

## Introduction
Welcome to my Angry Birds clone Project! To start this project; I created assets for the bird I wanted to use, the background, smoke, trail effect, retry button squares, wooden objects, the bow in two parts, the pig, the king pig, and the pig with glasses using Gemini.  

<img width="2000" height="600" alt="constructs" src="https://github.com/user-attachments/assets/0863c3f4-c0e2-4557-ac1f-20486f4abc3e" /><br>
<img width="2000" height="600" alt="image" src="https://github.com/user-attachments/assets/90806b22-c581-471b-bffc-a827bcddc029" /><br>
<img width="2000" height="600" alt="kingpin" src="https://github.com/user-attachments/assets/a038b405-b52e-469f-9422-adb1eb36b275" /><br> 
<img width="2000" height="600" alt="glassespig" src="https://github.com/user-attachments/assets/d9ea293a-c5eb-4c49-9b28-4322e7f73f0f" /><br><br>


Then, I cleaned their backgrounds using Adobe Photoshop and imported them into the Sprites folder I created in Unity. After that, I placed the background and created my boundaries using 4 quads. Then, I added the pigs, squares, and wooden objects to the scene. Later, I added a 2D Rigidbody and a 2D Collider to each of them.

## Features
I've implemented several features to the demo to add to the gameplay. For example, I've added:

### Idle animations
Idle animations for pigs, as well as the birds. I used the built-in animation feature of Unity to achieve this:

https://github.com/user-attachments/assets/ffde96bb-072f-499b-ad95-d7794fe82e82

https://github.com/user-attachments/assets/be60c2c5-ea1f-4330-a362-66b35a239b0a

### Gameplay features
I implemented core gameplay features such as:

#### Throwing the bird using the sling
```csharp
public class BirdLauncher2D : MonoBehaviour
{
    public float maxPullDistance = 3f;
    public float launchForce = 500f;

    private Vector2 startPosition;
    private bool isDragging = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 pullVector = mousePos - startPosition;

            if (pullVector.magnitude > maxPullDistance)
                pullVector = pullVector.normalized * maxPullDistance;

            rb.position = startPosition + pullVector;
        }

        if (isDragging && !Mouse.current.leftButton.isPressed)
        {
            isDragging = false;
            Vector2 launchVector = startPosition - rb.position;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(launchVector * launchForce);
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
```
#### Screen shake
```csharp
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
```
and
```csharp
public class CameraShake : MonoBehaviour
{
    public float duration = 0.2f;
    public float magnitude = 0.2f;

    Vector3 originalPos;

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine());
    }

    System.Collections.IEnumerator ShakeCoroutine()
    {
        originalPos = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
```
#### Damages to environment and to the pigs
```csharp
public class Destruction : MonoBehaviour
{
    public float resistance;
    public GameObject explosionPrefab;

    void OnCollisionEnter2D(Collision2D col)
    {
        float impactForce = col.relativeVelocity.magnitude;

        if (impactForce > resistance)
        {
            if (explosionPrefab != null)
            {
                GameObject go = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(go, 3f);
            }

            Destroy(gameObject, 0.1f);
        }
        else
        {
            resistance -= impactForce;
        }
    }
}
```
#### Reset button
```csharp
public class Restart : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
```
and I also added trail particle effects, smoke particle affects.

Thus, the gameplay I created was this:

https://github.com/user-attachments/assets/e0d28dc6-8d75-420a-8df0-c8f8ec193773

## Disclaimer
> [!NOTE]  
> This project is an independent demo inspired by Angry Birds. It is not affiliated with, endorsed by, or connected to Rovio Entertainment. All trademarks, characters, and original assets belong to their respective owners.
