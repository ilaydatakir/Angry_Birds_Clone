using UnityEngine;
using UnityEngine.InputSystem;

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