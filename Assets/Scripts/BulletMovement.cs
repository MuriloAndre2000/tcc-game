using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float movementSpeed = 20f; // Speed at which the character moves

    public int damage = 25;
    private void Update()
    {
        //Debug.Log(transform.rotation);
        transform.position = transform.position + transform.rotation * Vector3.forward * movementSpeed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collided with an enemy (you can use tags or layers to differentiate enemies)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the enemy's health component (you need to create an appropriate script on your enemy GameObjects)
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damage);
            }

            // Destroy the bullet GameObject
            Destroy(gameObject);        }
    }
}
