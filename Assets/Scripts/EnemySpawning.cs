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

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player") is not null){
            player = GameObject.FindWithTag("Player");
            player_exp = player.GetComponent<PlayerEXP>();
            stop_spawning = player_exp.return_pause_all();
            max_enemies = 20 + player_exp.return_level();
        }
        if (stop_spawning == false){
            if (TimeToRespawn >= 0){
                TimeToRespawn -= Time.deltaTime;
            }
            if (TimeToRespawn < 0f & GameObject.FindGameObjectsWithTag("Enemy").Length <= max_enemies){
                float[ ] x = new float[ ] {40.05f,
                                           40.05f,
                                           96f,
                                           95f,
                                           95f,
                                           -15f,
                                           -15f,
                                           -15f
                                                };
                float[ ] z = new float[ ] { 7.26f,
                                           -75.77f,
                                           -68f,
                                           -34f,
                                            0f,
                                            0f,
                                            -34f,
                                            -69f
                                                };
                int randomNumber = Random.Range(0, 8);
                Vector3 enemy_position = new Vector3 (x[randomNumber], 2, z[randomNumber]);
                GameObject newEnemy = Instantiate(enemyPrefab, enemy_position, Quaternion.identity);
                Destroy(newEnemy, 60);
                TimeToRespawn = 1.5f * Mathf.Pow(.99f, player_exp.return_level());
            }        
        }

    }
}
