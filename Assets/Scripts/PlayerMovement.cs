using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float initialMovementSpeed = 5f; // Speed at which the character moves
    public float movementSpeed; // Speed at which the character moves
    public float rotationSpeed = 10f; // Speed at which the character rotates

    PlayerWeaponChoosing player_weapon_choosing;

    WeaponMunitionHandler weapon_munition_handler;

    private Rigidbody rb;

    public LineRenderer lr;

    public GameObject bulletPrefab;
    public GameObject granadePrefab;
    public GameObject SelectWeapon;

    public AudioClip pistolShootingSound;
    public AudioClip machineGunShootingSound;
    public AudioClip shotgunShootingSound;
    private AudioSource m_AudioSource; //The thing to play the audio

    private GameObject Canvas;
    public GameObject PauseHandler;
    private PauseHandler pause_handler;
    private SelectWeaponLoading SelectWeaponLoadingObj;

    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, -1.5f);

    public float time_to_shoot_again = 0f;
    public float initial_time_to_shoot_again = 0f;

    private bool stop_moving = false;
    private GameObject player;   
    private PlayerEXP player_exp;

    private  Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start(){
        player = GameObject.FindWithTag("Player");
        player_exp = player.GetComponent<PlayerEXP>();

        Canvas = Camera.main.gameObject.transform.Find("Canvas").gameObject;
        pause_handler = PauseHandler.GetComponent<PauseHandler>();

        SelectWeaponLoadingObj = SelectWeapon.GetComponent<SelectWeaponLoading>();

        m_AudioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();

        weapon_munition_handler = gameObject.GetComponent<WeaponMunitionHandler>();
    }

    private void playShootingSound(AudioClip ShootingSound){
        m_AudioSource.clip = ShootingSound;
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
    }

    private void UpdateTimeToShotAgainUI(float time_to_shoot_again, float initial_time_to_shoot_again){
        if(initial_time_to_shoot_again != 0){
            SelectWeaponLoadingObj.ChangeFillScrollbar(1- (time_to_shoot_again/initial_time_to_shoot_again));
        }
        else{
            SelectWeaponLoadingObj.ChangeFillScrollbar(0f);
        }
    }

    private void Move(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
            
        if (movementDirection.sqrMagnitude > 1f){
            movementDirection.Normalize();
        }
        if (movementDirection.sqrMagnitude > 0f){
            animator.SetBool("is_walking", true);
        }
        else{
            animator.SetBool("is_walking", false);
        }
        int layerMask = 1 << LayerMask.NameToLayer("Untagged");
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(movementDirection), out hit, Mathf.Infinity, layerMask);
        //Debug.Log(hit.distance);
        // Estou tentando de alguma forma evitar que o Player atravesse paredes

        rb.MovePosition(rb.position + movementDirection * movementSpeed * Time.deltaTime);
    }

    private void Aim(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance)){
            worldPosition = ray.GetPoint(distance);
        }
        Vector3 start_ray = rb.position;
        Vector3 end_ray = worldPosition;
        Vector3[] points = {start_ray, end_ray, end_ray};
        
        if (PlayerWeaponChoosing.Weapon_ID == 4){
            Vector3 middle_ray = new Vector3((end_ray[0] + start_ray[0])/2,
                                             (end_ray[1] + start_ray[1])/2,
                                             (end_ray[2] + start_ray[2])/2);
            middle_ray += new Vector3(0f, 5f, 0f);
            points = new Vector3[] {start_ray, middle_ray, end_ray};
        }

        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPositions(points);

        Vector3 fromDirection = (start_ray - end_ray).normalized;

        fromDirection = new Vector3 (fromDirection[0], 0f, fromDirection[2]); 
        // O Vetor de slot 1 estava fazendo o player bugar quando o mouse estava perto dele porque a direção estava bugando

        Quaternion targetRotation = Quaternion.LookRotation(fromDirection);
        targetRotation *= Quaternion.Euler(0f, 180f, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private GameObject CreateBullet(Vector3 position, Quaternion direction, int base_damage, float time_to_shoot_again_base){
        GameObject newBullet = Instantiate(bulletPrefab, position, direction);
        int bullet_damage = player_exp.return_power_up_bullet_damage();
        float damage = (float) (1 + .5f * bullet_damage) * base_damage;
        newBullet.GetComponent<BulletBehavior>().damage = (int) damage;

        int bullet_size_increase = player_exp.return_power_up_bullet_size();
        float bullet_size_multiplier = (float) bullet_size_increase/10;
        newBullet.gameObject.transform.localScale += new Vector3(bullet_size_multiplier,
                                                                 bullet_size_multiplier,
                                                                 bullet_size_multiplier);

        Destroy(newBullet, 5);

        int fire_rate_increase = player_exp.return_power_up_fire_rate_increase();

        time_to_shoot_again = (float) time_to_shoot_again_base * Mathf.Pow(.9f, fire_rate_increase);
        initial_time_to_shoot_again = time_to_shoot_again;


        if (player_exp.return_power_up_explosive_bullet() > 0){
            BulletBehavior bullet_behavior = newBullet.GetComponent<BulletBehavior>();
            bullet_behavior.explosionForce = 100f;
            bullet_behavior.explosionRadius = .5f;
            bullet_behavior.explosionDamage = 5;
            bullet_behavior.is_explosive = true;
        }
        return newBullet;
    }



    private void FixedUpdate()
    {
        if (GameObject.FindWithTag("Player") is not null){
            stop_moving = pause_handler.GameIsPaused;
            movementSpeed = (float) (1 + .1f*player_exp.return_power_up_speed_increase())*initialMovementSpeed;
        }

        if (stop_moving == false){

            UpdateTimeToShotAgainUI(time_to_shoot_again, initial_time_to_shoot_again);

            Move();

            Aim();

            if(PlayerWeaponChoosing.Weapon_ID == 1){
                if (Input.GetMouseButton(0) & time_to_shoot_again ==0){
                    playShootingSound(pistolShootingSound);
                    Quaternion direction = transform.rotation;
                    CreateBullet(rb.position, direction, 30, .5f);
                }
            }
            if(PlayerWeaponChoosing.Weapon_ID == 2){
                if (Input.GetMouseButton(0) & time_to_shoot_again ==0 & weapon_munition_handler.machine_gun > 0){
                    Quaternion direction = transform.rotation;
                    float noiseStrife = Random.Range(0, 10);
                    direction *= Quaternion.Euler(0f, noiseStrife-5, 0f);

                    playShootingSound(machineGunShootingSound);
                    CreateBullet(rb.position, direction, 25, .25f);
                    weapon_munition_handler.machine_gun -=1;
                }
            }
            if(PlayerWeaponChoosing.Weapon_ID == 3){
                if (Input.GetMouseButton(0) & time_to_shoot_again ==0 & weapon_munition_handler.shotgun > 0){
                    float[] directions = {-20f,-10f,0f,10f,20f};

                    playShootingSound(shotgunShootingSound);

                    for(int i = 0; i < 5 ;i++){
                        Quaternion direction = transform.rotation;
                        direction *= Quaternion.Euler(0f, directions[i], 0f);
                        CreateBullet(rb.position, direction, 20, 1.5f);
                    }
                    weapon_munition_handler.shotgun -=1;
                }
            }

            if(PlayerWeaponChoosing.Weapon_ID == 4){
                if (Input.GetMouseButton(0) & time_to_shoot_again ==0 & weapon_munition_handler.granade > 0){
                    Quaternion direction = transform.rotation;
                    direction *= Quaternion.Euler(-45f, 0f, 0f);
                    GameObject newGrenade = CreateBullet(rb.position, direction, 0, 1f);
                    BulletBehavior grenade = newGrenade.GetComponent<BulletBehavior>();
                    grenade.explosionForce = 100f;
                    grenade.explosionRadius = 3f;
                    grenade.fuseTime = .5f;
                    grenade.is_grenade = true;
                    grenade.movementSpeed = 5f;
                    weapon_munition_handler.granade -=1;
                }
            }
            if(PlayerWeaponChoosing.Weapon_ID == 5){
                if (Input.GetMouseButton(0) & time_to_shoot_again ==0 & weapon_munition_handler.rpg > 0){
                    Quaternion direction = transform.rotation;
                    GameObject newGrenade = CreateBullet(rb.position, direction, 0, 1f);
                    BulletBehavior grenade = newGrenade.GetComponent<BulletBehavior>();
                    grenade.explosionForce = 100f;
                    grenade.explosionRadius = 3f;
                    grenade.is_explosive = true;
                    grenade.movementSpeed = 15f;
                    weapon_munition_handler.rpg -=1;
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
