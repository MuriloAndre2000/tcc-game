using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class SelectWeaponLoading : MonoBehaviour
{
    public GameObject Slider1UI;
    public GameObject Slider2UI;
    public GameObject Slider3UI;
    public GameObject Slider4UI;

    private Slider Slider1;
    private Slider Slider2;
    private Slider Slider3;
    private Slider Slider4;

    private float value_slider1;
    private float value_slider2;
    private float value_slider3;
    private float value_slider4;

    public float value_btw_0_1 = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Slider1 = Slider1UI.GetComponent<Slider>();
        Slider2 = Slider2UI.GetComponent<Slider>();
        Slider3 = Slider3UI.GetComponent<Slider>();
        Slider4 = Slider4UI.GetComponent<Slider>();
        value_slider1 = 0f;
        value_slider2 = 0f;
        value_slider3 = 0f;
        value_slider4 = 0f;
    }

    public void ChangeFillScrollbar(float value_btw_0_1){

        if (value_btw_0_1 > .25){
            value_slider1 = .25f;
            value_btw_0_1 -= .25f;
        }
        else{
            value_slider1 = value_btw_0_1;
            value_btw_0_1 = 0f;
        }

        if (value_btw_0_1 > .25){
            value_slider2 = .25f;
            value_btw_0_1 -= .25f;
        }
        else{
            value_slider2 = value_btw_0_1;
            value_btw_0_1 = 0f;
        }

        if (value_btw_0_1 > .25){
            value_slider3 = .25f;
            value_btw_0_1 -= .25f;
        }
        else{
            value_slider3 = value_btw_0_1;
            value_btw_0_1 = 0f;
        }

        if (value_btw_0_1 > .25){
            value_slider4 = .25f;
            value_btw_0_1 -= .25f;
        }
        else{
            value_slider4 = value_btw_0_1;
            value_btw_0_1 = 0f;
        }

        Slider1.value = value_slider1*4;
        Slider2.value = value_slider2*4;
        Slider3.value = value_slider3*4;
        Slider4.value = value_slider4*4;
    }
}
