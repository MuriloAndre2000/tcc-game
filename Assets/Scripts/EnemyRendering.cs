using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRendering : MonoBehaviour
{
    private float timeToRender = 3f;
    public float currentTimeToRender; 
    // Start is called before the first frame update
    void Start()
    {
        currentTimeToRender = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimeToRender > 0.0f){
            int LayerEnemy = LayerMask.NameToLayer("Enemy2");
            
            gameObject.layer = LayerEnemy;
            //Debug.Log("Current layer: " + gameObject.layer);
            currentTimeToRender -= Time.deltaTime;
        }
        else if (currentTimeToRender < 0){
            currentTimeToRender = 0.0f;
        }

        else if (currentTimeToRender == 0){
            int LayerIgnoreCamera = LayerMask.NameToLayer("Ignore Camera");
            gameObject.layer = LayerIgnoreCamera;
            //Debug.Log("Current layer: " + gameObject.layer);
        }
        
    }

    public void Render()
    {
        currentTimeToRender = timeToRender;
    }
}
