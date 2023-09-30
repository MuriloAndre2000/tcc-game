using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienProjectileMovement : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(5);
            }

            // Destroy the bullet GameObject
            Destroy(gameObject);        }
    }
}
