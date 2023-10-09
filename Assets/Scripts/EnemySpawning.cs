using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public float TimeToRespawn = 1f;

    public int max_enemies = 20;

    private GameObject player;
    private PlayerEXP player_exp;

    private bool stop_spawning = false;

    private int min_radius = 6;
    private int max_radius = 8;

    public int wave = 0;
    public int enemys_to_spawn_in_wave = 0;

    private Camera camera_1;
    private GameObject Canvas;

    [System.Serializable]
    public class wave_info{
        public string username;
        public int wave;
    }

    [System.Serializable]
    public class Info{
        public string username;
        public int accept;
    }

    private void Start()
    {
        camera_1 =  Camera.main;
        Canvas = camera_1.gameObject.transform.Find("Canvas").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player") is not null){
            player = GameObject.FindWithTag("Player");
            player_exp = player.GetComponent<PlayerEXP>();
            stop_spawning = player_exp.return_pause_all();
            max_enemies = 30 + player_exp.return_level();
            //Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);

            Canvas = camera_1.gameObject.transform.Find("Canvas").gameObject;
            GameObject Wave = Canvas.transform.Find("Wave").gameObject;
            TMPro.TextMeshProUGUI wave_text = Wave.GetComponent<TMPro.TextMeshProUGUI>();
            wave_text.text = "Wave " + wave;
        }
        else{
            stop_spawning = true;
        }
        if (stop_spawning == false){
            if (TimeToRespawn >= 0){
                TimeToRespawn -= Time.deltaTime;
            }
            if (enemys_to_spawn_in_wave == 0){
                if(GameObject.FindGameObjectsWithTag("Enemy").Length == 2){ // Apenas os Prefabs
                    wave += 1;
                    enemys_to_spawn_in_wave = (int) Mathf.Pow(3, wave);

                    string jsonText = File.ReadAllText(Application.persistentDataPath + "/credentials.json");
                    Info data = JsonUtility.FromJson<Info>(jsonText);
                    string username = data.username;

                    wave_info user_info = new wave_info();
                    user_info.username = username;
                    user_info.wave = wave;
                    string str_info = JsonUtility.ToJson(user_info);
                    string url = "https://southamerica-east1-disco-bedrock-364702.cloudfunctions.net/add_waves_played"; // Replace with your API endpoint
                    string json = str_info;
                    Debug.Log(json);
                    byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);

                    UnityWebRequest request = new UnityWebRequest(url, "POST");
                    request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonBytes);
                    request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                    request.SetRequestHeader("Content-Type", "application/json");



                    request.SendWebRequest();
                    /*
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        Debug.Log("POST request successful!");
                        Debug.Log(request.downloadHandler.text); // Response from server
                    }
                    else
                    {
                        Debug.LogError("Error: " + request.error);
                    }
                    */
                }    
            }
            if (TimeToRespawn < 0f 
                & GameObject.FindGameObjectsWithTag("Enemy").Length <= max_enemies
                & enemys_to_spawn_in_wave > 0){

                int enemy_choosen = 0;
                if (wave > 3){
                    int random_enemy = Random.Range(0,10);
                    if(random_enemy >=8){
                        enemy_choosen = 1;
                    }
                }

                if(enemy_choosen == 0){
                    float randomNumber_X = Random.Range(player.transform.position[0] + min_radius, 
                                                        player.transform.position[0] + max_radius);
                    
                    float randomNumber_Z = Random.Range(player.transform.position[2] + min_radius, 
                                                        player.transform.position[2] + max_radius);

                    int angle = Random.Range(0,360);
                    //Debug.Log(Mathf.Sin(angle));
                    randomNumber_X = Mathf.Sin(angle)* randomNumber_X;
                    randomNumber_Z = Mathf.Cos(angle)* randomNumber_Z;

                    Vector3 enemy_position = new Vector3 (randomNumber_X, 2, randomNumber_Z);

                    GameObject newEnemy = Instantiate(enemyPrefab, enemy_position, Quaternion.identity);
                    newEnemy.GetComponent<Alien1Movement>().is_spawned = true;
                    Destroy(newEnemy, 60);
                    TimeToRespawn = 1f * Mathf.Pow(.8f, player_exp.return_level() + wave);
                    enemys_to_spawn_in_wave -= 1;
                }
                if(enemy_choosen == 1){
                    float randomNumber_X = Random.Range(player.transform.position[0] + min_radius, 
                                                        player.transform.position[0] + max_radius);
                    
                    float randomNumber_Z = Random.Range(player.transform.position[2] + min_radius, 
                                                        player.transform.position[2] + max_radius);

                    int angle = Random.Range(0,360);
                    randomNumber_X = Mathf.Sin(angle)* randomNumber_X;
                    randomNumber_Z = Mathf.Sin(angle)* randomNumber_Z;

                    Vector3 enemy_position = new Vector3 (randomNumber_X, 2, randomNumber_Z);

                    GameObject newEnemy = Instantiate(enemyPrefab2, enemy_position, Quaternion.identity);
                    newEnemy.GetComponent<Alien2Movement>().is_spawned = true;
                    Destroy(newEnemy, 60);
                    TimeToRespawn = 1f * Mathf.Pow(.99f, player_exp.return_level() + wave);
                    enemys_to_spawn_in_wave -= 1;
                }
            }        
        }

    }
}
