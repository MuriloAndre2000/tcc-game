using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEXP : MonoBehaviour
{
    public int player_exp = 0;
    public int player_level = 0;
    public int level_step = 5;

    public bool pause_all = false;

    public int power_up_bullet_damage = 0; //DONE
    public int power_up_bullet_size = 0; //DONE
    public int power_up_health_increase = 0; //DONE
    public int power_up_fire_rate_increase = 0; //DONE
    public int power_up_speed_increase = 0; //DONE
    public int power_up_field_radius = 0; //DONE

    private Camera camera_1;
    private GameObject Canvas;
    private GameObject XP_points;

    private string option_1_text;
    private string option_2_text;

    private void Start()
    {
        camera_1 =  Camera.main;
        Canvas = camera_1.gameObject.transform.Find("Canvas").gameObject;
        //XP_Canvas = Canvas.gameObject.transform.Find("XP_Canvas").gameObject;
        //XP_Blue = XP_Canvas.gameObject.transform.Find("XP_Blue").gameObject;
        //XP_Bar = XP_Blue.GetComponent<RectTransform>();
        //XP_Bar.sizeDelta = new Vector2(350*player_exp,4);
        XP_points = Canvas.transform.Find("Points").gameObject;



        power_up_bullet_damage = (int) PlayerPrefs.GetFloat("DamageUpgrade");
        power_up_field_radius = (int) PlayerPrefs.GetFloat("RadiusUpgrade");
        power_up_fire_rate_increase = (int) PlayerPrefs.GetFloat("FireRateUpgrade");

    }

    // Update is called once per frame
    void Update()
    {
        if(player_exp > level_step){
            player_exp -= level_step;
            player_level += 1;
            pause_all = true;
            if (level_step < 12){
                level_step += 3;
            }
        }
        //XP_Bar.sizeDelta = new Vector2(40*player_exp,4);
        TMPro.TextMeshProUGUI points_text = XP_points.GetComponent<TMPro.TextMeshProUGUI>();
        points_text.text = "" + player_exp;

        render_options();
        check_input();
        assign_random_options();
        
    }

    private void assign_random_options(){

        if(pause_all == false){
            int randomNumber = Random.Range(0, 5);
            string[ ] options = new string[ ] {
                                                "power_up_bullet_damage", 
                                                "power_up_bullet_size", 
                                                "power_up_health_increase", 
                                                "power_up_fire_rate_increase", 
                                                //"power_up_speed_increase", 
                                                "power_up_field_radius"};
            
            option_1_text = options[randomNumber];
            option_2_text = options[randomNumber];
            while (option_1_text == option_2_text){
                randomNumber = Random.Range(0, 5);
                option_2_text = options[randomNumber];
            }
        }

    }

    private void check_input(){
        if (pause_all == true & Input.GetMouseButtonDown(0)){
            if(Camera.main.ScreenPointToRay(Input.mousePosition).direction[0] < 0){
                Debug.Log("Escolheu a opção 1");
                if(option_1_text == "power_up_bullet_damage"){
                    power_up_bullet_damage += 1;
                }
                if(option_1_text == "power_up_bullet_size"){
                    power_up_bullet_size += 1;
                }
                if(option_1_text == "power_up_health_increase"){
                    power_up_health_increase++;
                    if (GameObject.FindWithTag("Player") is not null){
                        GameObject player = GameObject.FindWithTag("Player");
                        PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
                        if (playerHealth != null)
                        {
                            // Apply damage to the enemy
                            playerHealth.AddHealth(15);
                        }
                    }
                }
                if(option_1_text == "power_up_fire_rate_increase"){
                    power_up_fire_rate_increase += 1;
                }
                if(option_1_text == "power_up_speed_increase"){
                    power_up_speed_increase += 1;
                }
                if(option_1_text == "power_up_field_radius"){
                    power_up_field_radius += 1;
                }
                pause_all = false;
            }
            if(Camera.main.ScreenPointToRay(Input.mousePosition).direction[0] > 0){
                Debug.Log("Escolheu a opção 2");
                if(option_2_text == "power_up_bullet_damage"){
                    power_up_bullet_damage++;
                }
                if(option_2_text == "power_up_bullet_size"){
                    power_up_bullet_size++;
                }
                if(option_2_text == "power_up_health_increase"){
                    power_up_health_increase++;
                    if (GameObject.FindWithTag("Player") is not null){
                        GameObject player = GameObject.FindWithTag("Player");
                        PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
                        if (playerHealth != null)
                        {
                            // Apply damage to the enemy
                            playerHealth.AddHealth(5);
                        }
                    }
                }
                if(option_2_text == "power_up_fire_rate_increase"){
                    power_up_fire_rate_increase++;
                }
                if(option_2_text == "power_up_speed_increase"){
                    power_up_speed_increase++;
                }
                if(option_2_text == "power_up_field_radius"){
                    power_up_field_radius++;
                }
                pause_all = false;
            }
        }
    }

    public bool return_pause_all(){
        return pause_all;
    }

    public int return_level(){
        return player_level;
    }

    public int return_power_up_bullet_damage(){
        return power_up_bullet_damage;
    }

    public int return_power_up_bullet_size(){
        return power_up_bullet_size;
    }

    public int return_power_up_health_increase(){
        return power_up_health_increase;
    }

    public int return_power_up_fire_rate_increase(){
        return power_up_fire_rate_increase;
    }

    public int return_power_up_speed_increase(){
        return power_up_speed_increase;
    }

    public int return_power_up_field_radius(){
        return power_up_field_radius;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collided with an enemy (you can use tags or layers to differentiate enemies)
        if (collision.gameObject.CompareTag("XP"))
        {
            player_exp += 1;
            //Debug.Log(1);
            // Destroy the bullet GameObject
            Destroy(collision.gameObject);        
        }
    }

    private void render_options()
    {
        Canvas = camera_1.gameObject.transform.Find("Canvas").gameObject;
        GameObject Option_1 = Canvas.transform.Find("Option_1").gameObject;
        RectTransform Option_1_Transform = Option_1.GetComponent<RectTransform>();
        GameObject Option_1_text_button = Option_1.transform.Find("Text_Option_1").gameObject;
        TMPro.TextMeshProUGUI Option_1_text_button_text = Option_1_text_button.GetComponent<TMPro.TextMeshProUGUI>();
        
        if(option_1_text == "power_up_bullet_damage"){
            Option_1_text_button_text.text = "Opção 1:\n 10% a mais de dano";
        }
        if(option_1_text == "power_up_bullet_size"){
            Option_1_text_button_text.text = "Opção 1:\n Balas maiores";
        }
        if(option_1_text == "power_up_health_increase"){
            Option_1_text_button_text.text = "Opção 1:\n Aumento da vida";
        }
        if(option_1_text == "power_up_fire_rate_increase"){
            Option_1_text_button_text.text = "Opção 1:\n Atirar mais rapido";
        }
        if(option_1_text == "power_up_speed_increase"){
            Option_1_text_button_text.text = "Opção 1:\n Aumento da velocidade";
        }
        if(option_1_text == "power_up_field_radius"){
            Option_1_text_button_text.text = "Opção 1:\n Aumento do raio de visão";
        }

        GameObject Option_2 = Canvas.transform.Find("Option_2").gameObject;
        RectTransform Option_2_Transform = Option_2.GetComponent<RectTransform>();
        GameObject Option_2_text_button = Option_2.transform.Find("Text_Option_2").gameObject;
        TMPro.TextMeshProUGUI Option_2_text_button_text = Option_2_text_button.GetComponent<TMPro.TextMeshProUGUI>();
        
        if(option_2_text == "power_up_bullet_damage"){
            Option_2_text_button_text.text = "Opção 2:\n 10% a mais de dano";
        }
        if(option_2_text == "power_up_bullet_size"){
            Option_2_text_button_text.text = "Opção 2:\n Balas maiores";
        }
        if(option_2_text == "power_up_health_increase"){
            Option_2_text_button_text.text = "Opção 2:\n Aumento da vida";
        }
        if(option_2_text == "power_up_fire_rate_increase"){
            Option_2_text_button_text.text = "Opção 2:\n Atirar mais rapido";
        }
        if(option_2_text == "power_up_speed_increase"){
            Option_2_text_button_text.text = "Opção 2:\n Aumento da velocidade";
        }
        if(option_2_text == "power_up_field_radius"){
            Option_2_text_button_text.text = "Opção 2:\n Aumento do raio de visão";
        }

        if(pause_all == false){
            Option_1_Transform.anchoredPosition  = new Vector2(-2000,0);
            Option_2_Transform.anchoredPosition  = new Vector2(2000,0);
        }
        else{
            Option_1_Transform.anchoredPosition  = new Vector2(-250,0);
            Option_2_Transform.anchoredPosition  = new Vector2(250,0);
        }
    }
}
