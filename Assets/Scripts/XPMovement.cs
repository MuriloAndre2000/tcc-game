using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPMovement : MonoBehaviour
{
    public Transform player_transform;
    public float movementSpeed = 5f;
    public float inital_movementSpeed = 5f;
    private float distance;
    private float rotationSpeed = 10f;

    private GameObject player;
    private PlayerEXP player_exp;

    public float min_distance_inital = 3f;
    public float distance_inital = 3f;

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
                if (distance <= distance_inital){
                    // Move the enemy towards the player
                    transform.position += directionToPlayer * movementSpeed * Time.deltaTime;
                    Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}
