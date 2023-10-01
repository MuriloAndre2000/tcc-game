using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float TimeToRespawn = 1f;

    public int max_enemies = 20;

    private GameObject player;
    private PlayerEXP player_exp;

    private bool stop_spawning = false;

    private int min_radius = 6;
    private int max_radius = 8;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player") is not null){
            player = GameObject.FindWithTag("Player");
            player_exp = player.GetComponent<PlayerEXP>();
            stop_spawning = player_exp.return_pause_all();
            max_enemies = 50 + player_exp.return_level();
        }
        if (stop_spawning == false){
            if (TimeToRespawn >= 0){
                TimeToRespawn -= Time.deltaTime;
            }
            if (TimeToRespawn < 0f & GameObject.FindGameObjectsWithTag("Enemy").Length <= max_enemies){
                float randomNumber_X = Random.Range(player.transform.position[0] + min_radius, 
                                                    player.transform.position[0] + max_radius);
                
                float randomNumber_Z = Random.Range(player.transform.position[2] + min_radius, 
                                                    player.transform.position[2] + max_radius);

                int angle = Random.Range(0,360);
                randomNumber_X = Mathf.Sin(angle)* randomNumber_X;
                randomNumber_Z = Mathf.Sin(angle)* randomNumber_Z;

                Vector3 enemy_position = new Vector3 (randomNumber_X, 2, randomNumber_Z);

                GameObject newEnemy = Instantiate(enemyPrefab, enemy_position, Quaternion.identity);
                Destroy(newEnemy, 60);
                TimeToRespawn = 1f * Mathf.Pow(.99f, player_exp.return_level());
            }        
        }

    }
}
