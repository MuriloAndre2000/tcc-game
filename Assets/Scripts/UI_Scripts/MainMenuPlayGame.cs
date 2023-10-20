using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPlayGame : MonoBehaviour
{
    public bool UpgradeIsOpen = false;

    public GameObject UpgradeMenuUI;
    public GameObject CountPointsUI;


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMainMenu(){
        UpgradeMenuUI.SetActive(false);
        UpgradeIsOpen = false;
    }

    public void OpenUpgradeMenu(){
        UpgradeMenuUI.SetActive(true);
        UpgradeIsOpen = true;
    }

    void Start(){
        if(!PlayerPrefs.HasKey("ResearchPoints")){
            PlayerPrefs.SetFloat("ResearchPoints", 0f);
        }
    }

    void Update(){
        TMPro.TextMeshProUGUI CountPoints_text = CountPointsUI.GetComponent<TMPro.TextMeshProUGUI>();
        CountPoints_text.text = PlayerPrefs.GetFloat("ResearchPoints") + " pontos";
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (UpgradeIsOpen){
                BackToMainMenu();
            }
        }
    }
}
