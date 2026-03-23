using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
