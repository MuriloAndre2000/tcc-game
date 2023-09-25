using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 3f;
    public Transform player_transform;
    private float distance;
    private float time_to_attack_again = 0f;

    private GameObject player;
    private PlayerEXP player_exp;


    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player") is not null){
            player = GameObject.FindWithTag("Player");
            player_exp = player.GetComponent<PlayerEXP>();

            if (player_exp.return_pause_all() == false) {
                player_transform = GameObject.FindWithTag("Player").transform;
                Vector3 directionToPlayer = player_transform.position - transform.position;
                distance = Vector3.Distance (player_transform.position, transform.position);

                directionToPlayer.Normalize();

                // Move the enemy towards the player
                transform.position += directionToPlayer * movementSpeed * Time.deltaTime;

                if (distance < 1.5 & time_to_attack_again == 0){
                    PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        // Apply damage to the enemy
                        playerHealth.TakeDamage(5);
                        time_to_attack_again = 1.5f;
                    }
                }

                if (time_to_attack_again < 0){
                    time_to_attack_again = 0f;
                }
                if (time_to_attack_again > 0){
                    time_to_attack_again -= Time.deltaTime;
                }
            }
        }
    }

}
