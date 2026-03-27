using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class BirdController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startPos;
    private SpriteRenderer sr;
    private ParticleSystem ps;
    private bool isDragging = false;

    public float launchPower = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();

        startPos = transform.position;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(
                Mouse.current.position.ReadValue());
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
            if (hit != null && hit.gameObject == gameObject)
            {
                isDragging = true;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }

        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(
                Mouse.current.position.ReadValue());
            mousePos.z = 0;
            transform.position = mousePos;
        }

        if (isDragging && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
            Vector2 direction = startPos - (Vector2)transform.position;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1;
            rb.AddForce(direction * launchPower, ForceMode2D.Impulse);
            StartCoroutine(ResetAfterLanding());
        }
    }

    IEnumerator ResetAfterLanding()
    {
        while (rb.linearVelocity.magnitude > 0.3f)
            yield return null;

        yield return new WaitForSeconds(3f);

        sr.enabled = false;
        if (ps != null)
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        transform.position = startPos;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
        sr.enabled = true;

        if (ps != null)
            ps.Play();
    }
}