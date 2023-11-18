using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float movementSpeed = 20f; // Speed at which the character moves
    public int damage = 25;

    public float explosionForce = 0f;
    public float explosionRadius = 0f;
    public float fuseTime = 3f;
    public int explosionDamage = 50;
    public ParticleSystem explosionEffect;

    public int fire_amount = 0;
    public int freeze_amount = 0;

    public bool is_explosive = false;
    public bool is_grenade   = false;
    private bool exploded = false;

    
    private float time_alive = 0f;

    private void Explode(){
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
                EnemyHealth enemyHealth = nearbyObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(explosionDamage);
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

    private void Update()
    {
        transform.position = transform.position + transform.rotation * Vector3.forward * movementSpeed * Time.deltaTime;
        if(is_grenade){
            time_alive += Time.deltaTime;
            if (time_alive > fuseTime){
                Explode();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damage);
            }

            if(is_grenade | is_explosive){
                Explode();
            }
            else{
                Destroy(gameObject);  
            }       
        }
        else if(collision.gameObject.CompareTag("Building")){
            if(is_grenade | is_explosive){
                Explode();
            }
            else{
                Destroy(gameObject);  
            }
        }
    }
}
