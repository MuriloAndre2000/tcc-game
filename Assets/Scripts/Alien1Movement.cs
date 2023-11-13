using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien1Movement : MonoBehaviour
{
    public float movementSpeed = 3f;
    private Transform player_transform;
    private float distance;
    private float time_to_attack = .5f;
    private float time_to_attack_again = 0f;
    private float rotationSpeed = 10f;

    private GameObject player;
    private PlayerEXP player_exp;
    public GameObject PauseHandler;
    private PauseHandler pause_handler;

    private UnityEngine.AI.NavMeshAgent agent;

    public bool is_spawned = false;

    void Start(){
        player = GameObject.FindWithTag("Player");
        player_exp = player.GetComponent<PlayerEXP>();
        pause_handler = PauseHandler.GetComponent<PauseHandler>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private bool CheckIfRayCastHitPlayer(Vector3 direction, int layerMask){
        RaycastHit hit;
        return Physics.Raycast(transform.position, 
                            transform.TransformDirection(direction), 
                            out hit, Mathf.Infinity, layerMask);
    }


    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player") is not null){
            if(is_spawned == true & transform.position.y < -100){
                Destroy(gameObject);
                EnemySpawning enemy_spawning = Camera.main.gameObject.GetComponent<EnemySpawning>();
                enemy_spawning.enemys_to_spawn_in_wave += 1;
            }

            if (pause_handler.GameIsPaused == false) {
                player_transform = GameObject.FindWithTag("Player").transform;
                Vector3 directionToPlayer = player_transform.position - transform.position;

                int layerMask = 1 << LayerMask.NameToLayer("Player");

                distance = Vector3.Distance (player_transform.position, transform.position);

                directionToPlayer.Normalize();

                agent.destination = player_transform.position; //Define the destination to the Player
                agent.updatePosition = false;

                // Move the enemy towards the player
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                if(CheckIfRayCastHitPlayer(directionToPlayer, layerMask)){ // If Alien is seeing the Player no need to use PathFinding
                    transform.position += directionToPlayer * movementSpeed * Time.deltaTime;
                }
                else{
                    transform.position = agent.nextPosition;
                }
                if (distance < 1.5){
                    if (time_to_attack < 0){
                        time_to_attack = 0f;
                    }
                    if (time_to_attack > 0){
                        time_to_attack -= Time.deltaTime;
                    }
                }
                else{
                    time_to_attack = .5f;
                }

                if (distance < 1.5 & time_to_attack == 0 & time_to_attack_again == 0){
                    PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        // Apply damage to the enemy
                        playerHealth.TakeDamage(5);
                        time_to_attack_again = 1.1f;
                        time_to_attack = .5f;
                    }
                }

                if (time_to_attack_again < 0){
                    time_to_attack_again = 0f;
                }
                if (time_to_attack_again > 0){
                    time_to_attack_again -= Time.deltaTime;
                }
            }
            else{
                player_transform = GameObject.FindWithTag("Player").transform;
                Vector3 directionToPlayer = player_transform.position - transform.position;

                int layerMask = 1 << LayerMask.NameToLayer("Player");

                distance = Vector3.Distance (player_transform.position, transform.position);

                directionToPlayer.Normalize();
                if(!CheckIfRayCastHitPlayer(directionToPlayer, layerMask)){ // If Alien is seeing the Player no need to use PathFinding
                    agent.destination = gameObject.transform.position;
                }
                agent.updatePosition = false;
            }
        }
    }

}
