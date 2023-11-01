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
        Debug.Log(frontiers);
        activate = false;
        foreach(GameObject frontier in frontiers)
        {
           Transform object_transform = frontier.transform;
           float distance = Vector3.Distance(object_transform.position, transform.position);
           if (distance < 4){
                activate = true;
           }
        } 
        BuyText.SetActive(activate);

    }
}
