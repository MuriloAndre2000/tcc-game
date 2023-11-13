using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenVolume : MonoBehaviour
{
    public GameObject sound_slider;
    public bool slider_open = false;

    public void Open_Slider(){
        if (slider_open == false){
            slider_open = true;
        }
        else{
            slider_open = false;
        }
    }

    void Update(){
        sound_slider.SetActive(slider_open);
    }
}
