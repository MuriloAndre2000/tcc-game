using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFrontierText : MonoBehaviour
{
    public GameObject BuyText;
    private bool activate = false;

    // Update is called once per frame
    void Update()
    {
        GameObject[] frontiers = GameObject.FindGameObjectsWithTag("Frontier");
        activate = false;
        foreach(GameObject frontier in frontiers)
        {
           Transform object_transform = frontier.transform;
           float distance = Vector3.Distance(object_transform.position, transform.position);
           if (distance < 4){
                activate = true;
                if(Input.GetKeyDown("e")){
                    PlayerEXP player_exp = gameObject.GetComponent<PlayerEXP>();
                    if (player_exp.player_exp >=30){
                        player_exp.player_exp -= 30;
                        frontier.SetActive(false);
                    }
                }
           }
        } 
        BuyText.SetActive(activate);

    }
}
