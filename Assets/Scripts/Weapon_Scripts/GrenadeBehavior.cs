using UnityEngine;

public class GrenadeBehavior : MonoBehaviour
{
    public float explosionForce = 1000f;
    public float explosionRadius = 10f;
    public float fuseTime = 3f;
    public ParticleSystem explosionEffect;
    
    private bool exploded = false;

    void Start()
    {
        Invoke("Explode", fuseTime);
    }

    void Explode()
    {
        if (!exploded)
        {
            exploded = true;

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 3.0F);
                }
            }

            // Instantiate explosion effect
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
