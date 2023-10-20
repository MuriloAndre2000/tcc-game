using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float initialMovementSpeed = 5f; // Speed at which the character moves
    public float movementSpeed; // Speed at which the character moves
    public float rotationSpeed = 10f; // Speed at which the character rotates

    private Rigidbody rb;

    public LineRenderer lr;

    public GameObject bulletPrefab;
    public GameObject granadePrefab;
    public GameObject SelectWeapon;


    private GameObject Canvas;
    private GameObject Pause_Menu;
    private PauseMenu PauseMenu_Object;
    private SelectWeaponLoading SelectWeaponLoadingObj;

    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, -1.5f);

    public float time_to_shoot_again = 0f;
    public float initial_time_to_shoot_again = 0f;

    private bool stop_moving = false;
    private GameObject player;   
    private PlayerEXP player_exp;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start(){
        player = GameObject.FindWithTag("Player");
        player_exp = player.GetComponent<PlayerEXP>();

        Canvas = Camera.main.gameObject.transform.Find("Canvas").gameObject;
        Pause_Menu = Camera.main.gameObject.transform.Find("Pause").gameObject;
        PauseMenu_Object = Pause_Menu.GetComponent<PauseMenu>();

        SelectWeaponLoadingObj = SelectWeapon.GetComponent<SelectWeaponLoading>();
    }


    private void Update()
    {
        if (GameObject.FindWithTag("Player") is not null){
            stop_moving = player_exp.return_pause_all() | PauseMenu_Object.IsGamePaused();
            movementSpeed = (float) (1 + .1f*player_exp.return_power_up_speed_increase())*initialMovementSpeed;
        }

        if (stop_moving == false){

            if(initial_time_to_shoot_again != 0){
                SelectWeaponLoadingObj.ChangeFillScrollbar(time_to_shoot_again/initial_time_to_shoot_again);
            }
            else{
                SelectWeaponLoadingObj.ChangeFillScrollbar(0f);
            }

            //Move
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
            rb.MovePosition(rb.position + movementDirection * movementSpeed * Time.deltaTime);

            //if (movementDirection != Vector3.zero)
            //{
            //    Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            //}

            //Trying to understand mouse in In unity
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance)){
                worldPosition = ray.GetPoint(distance);
            }
            Vector3 start_ray = rb.position;
            Vector3 end_ray = worldPosition;

            lr = GetComponent<LineRenderer>();
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.SetPosition(0, start_ray);
            lr.SetPosition(1, end_ray);

            Vector3 fromDirection = (start_ray - end_ray).normalized;

            fromDirection = new Vector3 (fromDirection[0], 0f, fromDirection[2]); 
            // O Vetor de slot 1 estava fazendo o player bugar quando o mouse estava perto dele porque a direção estava bugando

            Quaternion targetRotation = Quaternion.LookRotation(fromDirection);
            targetRotation *= Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if(PlayerWeaponChoosing.Weapon_ID == 1){
                if (Input.GetMouseButton(0) & time_to_shoot_again ==0){
                    //fromDirection = (start_ray - end_ray).normalized;
                    Quaternion direction = Quaternion.FromToRotation(Vector3.forward,fromDirection);
                    direction *= Quaternion.Euler(0f, 180f, 0f);

                    Vector3 bullet_position = rb.position + fromDirection*-1;

                    GameObject newBullet = Instantiate(bulletPrefab, bullet_position, direction);
                    newBullet.GetComponent<BulletMovement>().damage = (int) (1 + .5f*player_exp.return_power_up_bullet_damage()) *30;
                    int bullet_size_increase = player_exp.return_power_up_bullet_size();
                    newBullet.gameObject.transform.localScale += new Vector3(bullet_size_increase/10,bullet_size_increase/10,bullet_size_increase/10);
                    Destroy(newBullet, 5);
                    time_to_shoot_again = (float) .5f * Mathf.Pow(.9f, player_exp.return_power_up_fire_rate_increase());
                    initial_time_to_shoot_again = time_to_shoot_again;
                }
            }
            if(PlayerWeaponChoosing.Weapon_ID == 2){
                if (Input.GetMouseButton(0) & time_to_shoot_again ==0){
                    //fromDirection = (start_ray - end_ray).normalized;
                    Quaternion direction = Quaternion.FromToRotation(Vector3.forward,fromDirection);
                    direction *= Quaternion.Euler(0f, 180f, 0f);

                    Vector3 bullet_position = rb.position + fromDirection*-1;

                    GameObject newBullet = Instantiate(bulletPrefab, bullet_position, direction);
                    newBullet.GetComponent<BulletMovement>().damage = (int) (1 + .5f*player_exp.return_power_up_bullet_damage()) * 25;

                    int bullet_size_increase = player_exp.return_power_up_bullet_size();
                    newBullet.gameObject.transform.localScale += new Vector3(bullet_size_increase/10,bullet_size_increase/10,bullet_size_increase/10);
                    
                    Destroy(newBullet, 5);
                    time_to_shoot_again = (float) .25f * Mathf.Pow(.9f, player_exp.return_power_up_fire_rate_increase());;
                    initial_time_to_shoot_again = time_to_shoot_again;
                }
            }
            if(PlayerWeaponChoosing.Weapon_ID == 3){
                if (Input.GetMouseButton(0) & time_to_shoot_again ==0){
                    float[] directions = {-20f,-10f,0f,10f,20f};
                        for(int i = 0; i < 5 ;i++){
                            //fromDirection = (start_ray - end_ray).normalized;
                            Quaternion direction = Quaternion.FromToRotation(Vector3.forward,fromDirection);
                            direction *= Quaternion.Euler(0f, 180f, 0f);
                            direction *= Quaternion.Euler(0f, directions[i], 0f);

                            Vector3 bullet_position = rb.position + fromDirection*-1;

                            GameObject newBullet = Instantiate(bulletPrefab, bullet_position, direction);
                            newBullet.GetComponent<BulletMovement>().damage = (int) (1 + .5f*player_exp.return_power_up_bullet_damage()) * 20;
                            
                            int bullet_size_increase = player_exp.return_power_up_bullet_size();
                            newBullet.gameObject.transform.localScale += new Vector3(bullet_size_increase/10,bullet_size_increase/10,bullet_size_increase/10);
                    
                            Destroy(newBullet, 5);
                    }
                    time_to_shoot_again = (float) 1.5f * Mathf.Pow(.9f, player_exp.return_power_up_fire_rate_increase());;
                    initial_time_to_shoot_again = time_to_shoot_again;
                }
            }

            if(PlayerWeaponChoosing.Weapon_ID == 4){
                if (Input.GetMouseButton(0) & time_to_shoot_again ==0){
                    fromDirection = (start_ray - end_ray).normalized;
                    Quaternion direction = Quaternion.FromToRotation(Vector3.forward,fromDirection);
                    direction *= Quaternion.Euler(0f, 180f, 0f);

                    Vector3 bullet_position = rb.position + fromDirection*-1;

                    GameObject newGranade = Instantiate(granadePrefab, bullet_position, direction);
                    //newGranade.GetComponent<BulletMovement>().damage = (int) (1 + .5f*player_exp.return_power_up_bullet_damage()) * 25;

                    int granade_size_increase = player_exp.return_power_up_bullet_size();
                    newGranade.gameObject.transform.localScale += new Vector3(granade_size_increase/10,granade_size_increase/10,granade_size_increase/10);
                    
                    Destroy(newGranade, 5);
                    time_to_shoot_again = (float) 2f * Mathf.Pow(.9f, player_exp.return_power_up_fire_rate_increase());;
                    initial_time_to_shoot_again = time_to_shoot_again;
                }
            }

            if (time_to_shoot_again < 0){
                time_to_shoot_again = 0f;
                initial_time_to_shoot_again = 0f;
            }
            if (time_to_shoot_again > 0){
                time_to_shoot_again -= Time.deltaTime;
            }
        }
    }
}
