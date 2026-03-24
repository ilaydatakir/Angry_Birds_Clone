using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startPos;
    private SpriteRenderer sr;
    private ParticleSystem ps;

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

    void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }

    void OnMouseUp()
    {
        Vector2 direction = startPos - (Vector2)transform.position;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;
        rb.AddForce(direction * launchPower, ForceMode2D.Impulse);

        StartCoroutine(ResetAfterLanding());
    }

    IEnumerator ResetAfterLanding()
    {
        // Kuş hareket ediyorsa bekle
        while (rb.linearVelocity.magnitude > 0.3f)
            yield return null;

        yield return new WaitForSeconds(3f);

        // Kuş ve partiküller kaybolur / durur
        sr.enabled = false;
        if (ps != null)
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // Yerde 3 saniye bekle


        // Başlangıç pozisyonuna ışınla ve tekrar görünür yap
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