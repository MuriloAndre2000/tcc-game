using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{

    public GameObject LevelDamagePointsUI;
    public GameObject ButtonDamagePointsUI;
    public GameObject UpgradeDamagePointsUI;

    public GameObject LevelRadiusPointsUI;
    public GameObject ButtonRadiusPointsUI;
    public GameObject UpgradeRadiusPointsUI;

    public GameObject LevelFireRatePointsUI;
    public GameObject ButtonFireRatePointsUI;
    public GameObject UpgradeFireRatePointsUI;


    public void BuyDamageUpgrade(){
        float damageLevel = PlayerPrefs.GetFloat("DamageUpgrade");
        float pointsFromPlayer = PlayerPrefs.GetFloat("ResearchPoints");
        if (pointsFromPlayer >= 2*(1 + damageLevel)){
            PlayerPrefs.SetFloat("ResearchPoints", pointsFromPlayer - 2*(1 + damageLevel));
            PlayerPrefs.SetFloat("DamageUpgrade", damageLevel + 1);
        }
    }

    public void BuyRadiusUpgrade(){
        float radiusLevel = PlayerPrefs.GetFloat("RadiusUpgrade");
        if (radiusLevel> 3){
            return;
        }
        float pointsFromPlayer = PlayerPrefs.GetFloat("ResearchPoints");
        if (pointsFromPlayer >= 2*(1 + radiusLevel)){
            PlayerPrefs.SetFloat("ResearchPoints", pointsFromPlayer - 2*(1 + radiusLevel));
            PlayerPrefs.SetFloat("RadiusUpgrade", radiusLevel + 1);
        }
    }

    public void BuyFireRateUpgrade(){
        float fireRateLevel = PlayerPrefs.GetFloat("FireRateUpgrade");
        if (fireRateLevel> 5){
            return;
        }
        float pointsFromPlayer = PlayerPrefs.GetFloat("ResearchPoints");
        if (pointsFromPlayer >= 2*(1 + fireRateLevel)){
            PlayerPrefs.SetFloat("ResearchPoints", pointsFromPlayer - 2*(1 + fireRateLevel));
            PlayerPrefs.SetFloat("FireRateUpgrade", fireRateLevel + 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("DamageUpgrade")){
            PlayerPrefs.SetFloat("DamageUpgrade", 0f);
        }
        if(!PlayerPrefs.HasKey("RadiusUpgrade")){
            PlayerPrefs.SetFloat("RadiusUpgrade", 0f);
        } 
        if(!PlayerPrefs.HasKey("FireRateUpgrade")){
            PlayerPrefs.SetFloat("FireRateUpgrade", 0f);
        }   
    }

    void UpdateDamage(){
        TMPro.TextMeshProUGUI LevelDamagePoints_text = LevelDamagePointsUI.GetComponent<TMPro.TextMeshProUGUI>();
        LevelDamagePoints_text.text = PlayerPrefs.GetFloat("DamageUpgrade") + "/5";

        TMPro.TextMeshProUGUI UpgradeDamagePoints_text = UpgradeDamagePointsUI.GetComponent<TMPro.TextMeshProUGUI>();
        if(PlayerPrefs.GetFloat("DamageUpgrade") <= 5){
            UpgradeDamagePoints_text.text = 2*(1 + PlayerPrefs.GetFloat("DamageUpgrade")) + " pontos";
        }
        if(PlayerPrefs.GetFloat("DamageUpgrade") > 5){
            UpgradeDamagePointsUI.SetActive(false);
            ButtonDamagePointsUI.SetActive(false);
        }
    }

    void UpdateRadius(){
        TMPro.TextMeshProUGUI LevelRadiusPoints_text = LevelRadiusPointsUI.GetComponent<TMPro.TextMeshProUGUI>();
        LevelRadiusPoints_text.text = PlayerPrefs.GetFloat("RadiusUpgrade") + "/3";

        TMPro.TextMeshProUGUI UpgradeRadiusPoints_text = UpgradeRadiusPointsUI.GetComponent<TMPro.TextMeshProUGUI>();
        if(PlayerPrefs.GetFloat("RadiusUpgrade") <= 3){
            UpgradeRadiusPoints_text.text = 2*(1 + PlayerPrefs.GetFloat("RadiusUpgrade")) + " pontos";
        }
        if(PlayerPrefs.GetFloat("RadiusUpgrade") > 3){
            UpgradeRadiusPointsUI.SetActive(false);
            ButtonRadiusPointsUI.SetActive(false);
        }
    }
    void UpdateFireRate(){
        TMPro.TextMeshProUGUI LevelFireRatePoints_text = LevelFireRatePointsUI.GetComponent<TMPro.TextMeshProUGUI>();
        LevelFireRatePoints_text.text = PlayerPrefs.GetFloat("FireRateUpgrade") + "/5";

        TMPro.TextMeshProUGUI UpgradeFireRatePoints_text = UpgradeFireRatePointsUI.GetComponent<TMPro.TextMeshProUGUI>();
        if(PlayerPrefs.GetFloat("FireRateUpgrade") <= 5){
            UpgradeFireRatePoints_text.text = 2*(1 + PlayerPrefs.GetFloat("FireRateUpgrade")) + " pontos";
        }
        if(PlayerPrefs.GetFloat("FireRateUpgrade") > 5){
            UpgradeFireRatePointsUI.SetActive(false);
            ButtonFireRatePointsUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDamage();
        UpdateRadius();
        UpdateFireRate();
    }
}
