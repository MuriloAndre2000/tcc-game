using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien2Movement : MonoBehaviour
{
    public float movementSpeed = 3f;
    public Transform player_transform;
    private float distance;
    private float time_to_attack_again = 0f;
    private float rotationSpeed = 10f;

    private GameObject player;
    private PlayerEXP player_exp;

    public GameObject projectilePrefab;



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
                if (distance > 5 & distance < 100){
                    // Move the enemy towards the player
                    transform.position += directionToPlayer * movementSpeed * Time.deltaTime;
                    Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
                if (distance < 5 & time_to_attack_again == 0){
                    Quaternion direction = Quaternion.FromToRotation(Vector3.forward,directionToPlayer);

                    Vector3 bullet_position = transform.position + directionToPlayer*2;

                    GameObject newProjectile = Instantiate(projectilePrefab, bullet_position, direction);

                    Destroy(newProjectile, 5);
                    time_to_attack_again = 1.5f;
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
