using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;


public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public float TimeToRespawn = 1f;

    public int max_enemies = 20;

    private GameObject player;
    private PlayerEXP player_exp;

    private bool stop_spawning = false;

    public GameObject PowerUpsHandler;
    private OpenPowerUp open_power_up;

    public int wave = 0;
    public int enemys_to_spawn_in_wave = 0;

    private GameObject Canvas;
    private GameObject Pause_Menu;
    private PauseMenu PauseMenu_Object;
    private GameObject Wave;

    public GameObject PauseHandler;
    private PauseHandler pause_handler;

    private GameObject[] enemy_spawning;

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
        Canvas = Camera.main.gameObject.transform.Find("Canvas").gameObject;

        pause_handler = PauseHandler.GetComponent<PauseHandler>();

        Pause_Menu = Camera.main.gameObject.transform.Find("Pause").gameObject;
        PauseMenu_Object = Pause_Menu.GetComponent<PauseMenu>();
        
        open_power_up = PowerUpsHandler.GetComponent<OpenPowerUp>();

        enemy_spawning = GameObject.FindGameObjectsWithTag("Enemy Portal");

        player = GameObject.FindWithTag("Player");
        player_exp = player.GetComponent<PlayerEXP>();

        Wave = Canvas.transform.Find("Wave").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player") is not null){
            enemy_spawning = GameObject.FindGameObjectsWithTag("Enemy Portal");
            stop_spawning = player_exp.return_pause_all() | PauseMenu_Object.IsGamePaused();
            
            max_enemies = 30;

            TMPro.TextMeshProUGUI wave_text = Wave.GetComponent<TMPro.TextMeshProUGUI>();
            wave_text.text = "Wave " + wave;
        }
        else{
            stop_spawning = true;
        }
        if (pause_handler.GameIsPaused == false){
            if (TimeToRespawn >= 0){
                TimeToRespawn -= Time.deltaTime;
            }
            if (enemys_to_spawn_in_wave == 0){
                if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0){ // Apenas os Prefabs
                    wave += 1;
                    enemys_to_spawn_in_wave = (int) 2 + (wave + 1) * (wave + 1);
                    
                    if(wave != 1){
                        open_power_up.OpenPowerUpMenu();
                    }

                    //string jsonText = File.ReadAllText(Application.persistentDataPath + "/credentials.json");
                    //Info data = JsonUtility.FromJson<Info>(jsonText);
                    //string username = data.username;
                    string username = PlayerPrefs.GetString("username");

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
                }    
            }
            else if(enemys_to_spawn_in_wave < 0){
                enemys_to_spawn_in_wave = 0;
            }
            if (TimeToRespawn < 0f 
                & GameObject.FindGameObjectsWithTag("Enemy").Length <= max_enemies
                & enemys_to_spawn_in_wave > 0){

                int enemy_choosen = 0;
                if (wave >= 4 & wave <= 6){
                    int random_enemy = Random.Range(0,10);
                    if(random_enemy >=6){
                        enemy_choosen = 1;
                    }
                }
                if(wave >= 7 & wave <= 100){
                    int random_enemy = Random.Range(0,10);
                    if(random_enemy >=5 & random_enemy <=7){
                        enemy_choosen = 1;
                    }
                    if(random_enemy >=9){
                        enemy_choosen = 2;
                    }
                }

                if(enemy_choosen == 0){
                    int index = Random.Range(0, enemy_spawning.Length);
                    float randomNumber_X = enemy_spawning[index].transform.position[0];
                    
                    float randomNumber_Z = enemy_spawning[index].transform.position[2];

                    Vector3 enemy_position = new Vector3 (randomNumber_X, 2, randomNumber_Z);

                    GameObject newEnemy = Instantiate(enemyPrefab, enemy_position, Quaternion.identity);
                    newEnemy.GetComponent<Alien1Movement>().is_spawned = true;
                    TimeToRespawn = 1f * Mathf.Pow(.8f, player_exp.return_level() + wave);
                    enemys_to_spawn_in_wave -= 1;
                }
                if(enemy_choosen == 1){
                    int index = Random.Range(0, enemy_spawning.Length);
                    float randomNumber_X = enemy_spawning[index].transform.position[0];
                    
                    float randomNumber_Z = enemy_spawning[index].transform.position[2];

                    Vector3 enemy_position = new Vector3 (randomNumber_X, 2, randomNumber_Z);

                    GameObject newEnemy = Instantiate(enemyPrefab2, enemy_position, Quaternion.identity);
                    newEnemy.GetComponent<Alien2Movement>().is_spawned = true;
                    TimeToRespawn = 1f * Mathf.Pow(.99f, player_exp.return_level() + wave);
                    enemys_to_spawn_in_wave -= 1;
                }
                if(enemy_choosen == 2){
                    int index = Random.Range(0, enemy_spawning.Length);
                    float randomNumber_X = enemy_spawning[index].transform.position[0];
                    
                    float randomNumber_Z = enemy_spawning[index].transform.position[2];

                    Vector3 enemy_position = new Vector3 (randomNumber_X, 2, randomNumber_Z);

                    GameObject newEnemy = Instantiate(enemyPrefab3, enemy_position, Quaternion.identity);
                    newEnemy.GetComponent<Alien1Movement>().is_spawned = true;
                    TimeToRespawn = 1f * Mathf.Pow(.99f, player_exp.return_level() + wave);
                    enemys_to_spawn_in_wave -= 3;
                }
            }        
        }

    }
}
