using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public StatsUI statsUI;
    public GameObject shopUI;
    public Button toggleButton;
    TMP_Text toggleButtonText;
    public Vector3 buttonOffset;
    string activeString = "close";
    string inactiveString = "open";
    string clickAudioName = "ButtonClick";
    Animator animator;

    public TurretBlueprint crossbowTurret;
    public TurretBlueprint cannonTurret;
    public TurretBlueprint laserBeamerTurret;
    public TurretBlueprint laserTurret;
    //TODO
    //public TurretBlueprint antibioticTurret;
    //public TurretBlueprint medicineTurret;

    public BuildManager buildManager;

    public GameObject crossbowTurretItem;
    public GameObject cannonTurretItem;
    Button cannonButton;
    Image cannonImage;
    public GameObject laserBeamerTurretItem;
    Button laserBeamerButton;
    Image laserBeamerImage;
    public GameObject laserTurretItem;
    Button laserButton;
    Image laserImage;
    public GameObject antibioticTurretItem;
    Button antibioticButton;
    Image antibioticImage;
    public GameObject medicineTurretItem;
    Button medicineButton;
    Image medicineImage;

    Dictionary<string, Sprite> itemsImages;
    public Sprite cannonLockedImage;
    public Sprite laserBeamerLockedImage;
    public Sprite laserLockedImage; 
    
    public int scoreToUnlockCannon;
    public int scoreToUnlockLaserBeamer;
    public int scoreToUnlockLaser;
    public int scoreToUnlockAntibiotic;
    public int scoreToUnlockMedicine;

    private void Start()
    {
        shopUI.SetActive(false);
        animator = GetComponent<Animator>();
        
        toggleButtonText = toggleButton.GetComponentInChildren<TMP_Text>();
        var shopUIState = shopUI.activeSelf;
        if (shopUIState)
        {
            toggleButtonText.text = activeString;
        }
        else
        {
            toggleButtonText.text = inactiveString;
        }

        FindObjectOfType<PlayerStats>().OnPlayerScoreChanged += HandlePlayerScoreChange;

        itemsImages = new Dictionary<string, Sprite>();

        itemsImages["cannon"] = cannonTurretItem.GetComponent<Image>().sprite;
        itemsImages["laserBeamer"] = laserBeamerTurretItem.GetComponent<Image>().sprite;
        itemsImages["laser"] = laserTurretItem.GetComponent<Image>().sprite;
        itemsImages["antibiotic"] = antibioticTurretItem.GetComponent<Image>().sprite;
        itemsImages["medicine"] = medicineTurretItem.GetComponent<Image>().sprite;

        cannonImage = cannonTurretItem.GetComponent<Image>();
        cannonButton = cannonTurretItem.GetComponent<Button>();

        laserBeamerImage = laserBeamerTurretItem.GetComponent<Image>();
        laserBeamerButton = laserBeamerTurretItem.GetComponent<Button>();

        laserImage = laserTurretItem.GetComponent<Image>();
        laserButton = laserTurretItem.GetComponent<Button>();

        buildManager = BuildManager.Instance;

        crossbowTurretItem.GetComponentInChildren<Text>().text = "$" + crossbowTurret.cost;
        cannonTurretItem.GetComponentInChildren<Text>().text = "$" + cannonTurret.cost;
        laserBeamerTurretItem.GetComponentInChildren<Text>().text = "$" + laserBeamerTurret.cost;
        laserTurretItem.GetComponentInChildren<Text>().text = "$" + laserTurret.cost;
        //antibioticTurretItem.GetComponentInChildren<Text>().text = "$" + antibioticTurret.cost;
        //medicineTurretItem.GetComponentInChildren<Text>().text = "$" + medicineTurret.cost;


        if (scoreToUnlockCannon == 0)
        {
            cannonImage.sprite = itemsImages["cannon"];
            cannonButton.interactable = true;
        }
        else
        {
            cannonImage.sprite = cannonLockedImage;
            cannonButton.interactable = false;
        }
        if (scoreToUnlockLaserBeamer == 0)
        {
            laserBeamerImage.sprite = itemsImages["laserBeamer"];
            laserBeamerButton.interactable = true;
        }
        else
        {
            laserBeamerImage.sprite = laserBeamerLockedImage;
            laserBeamerButton.interactable = false;
        }
        if (scoreToUnlockLaser == 0)
        {
            laserImage.sprite = itemsImages["laser"];
            laserButton.interactable = true;
        }
        else
        {
            laserImage.sprite = laserLockedImage;
            laserButton.interactable = false;
        }
        //if (scoreToUnlockAntibiotic == 0)
        //{
        //    laserImage.sprite = itemsImages["antibiotic"];
        //    antibioticButton.interactable = true;
        //}
        //if (scoreToUnlockMedicine == 0)
        //{
        //    laserImage.sprite = itemsImages["medicine"];
        //    medicineButton.interactable = true;
        //}

    }

    void HandlePlayerScoreChange(int score)
    {
        if (score == 0)
        {
            cannonImage.sprite = cannonLockedImage;
            laserBeamerImage.sprite = laserBeamerLockedImage;
            laserImage.sprite = laserLockedImage;

            cannonButton.interactable = false;
            laserButton.interactable = false;
            laserBeamerButton.interactable = false;
            //TODO REST
        }
        if (score >= scoreToUnlockMedicine)
        {
            //TODO
        }
        if(score >= scoreToUnlockAntibiotic)
        {
            //TODO
        }
        if(score >= scoreToUnlockLaser)
        {
            laserImage.sprite = itemsImages["laser"];
            laserButton.interactable = true;
        }
        if(score >= scoreToUnlockLaserBeamer)
        {
            laserBeamerImage.sprite = itemsImages["laserBeamer"];
            laserBeamerButton.interactable = true;
        }
        if(score >= scoreToUnlockCannon)
        {
            cannonImage.sprite = itemsImages["cannon"];
            cannonButton.interactable = true;
        }
    }

    public void SelectCrossbowTurret()
    {
        if(buildManager == null)
        {
            buildManager = BuildManager.Instance;
        }
        buildManager.SelectTurretToBuild(crossbowTurret);
    }
    public void MouseOverCrossbowTurret()
    {
        ShowStatsUI(crossbowTurret);
    }

    public void SelectCannonTurret()
    {
        buildManager.SelectTurretToBuild(cannonTurret);
    }
    public void MouseOverCannonTurret()
    {
        ShowStatsUI(cannonTurret);
    }

    public void SelectLaserBeamerTurret()
    {
        buildManager.SelectTurretToBuild(laserBeamerTurret);
    }
    public void MouseOverLaserBeamerTurret()
    {
        ShowStatsUI(laserBeamerTurret);
    }

    public void SelectLaserTurret()
    {
        buildManager.SelectTurretToBuild(laserTurret);
    }
    public void MouseOverLaserTurret()
    {
        ShowStatsUI(laserTurret);
    }
    /* 
     * TODO
    public void SelectAntibioticTurret()
    {

    }

    public void MouseOverAntibioticTurret()
    {

    }

    public void SelectMedicineTurret()
    {

    }

    public void MouseOverMedicineTurret()
    {

    }
    */

    public void ToggleShopPanel()
    {
        AudioManager.Instance.Play(clickAudioName);
        if(shopUI.activeSelf)
        {
            toggleButtonText.text = inactiveString;
            toggleButton.transform.position += buttonOffset;
            animator.SetTrigger("Out");

        } else
        {
            shopUI.SetActive(true);
            toggleButtonText.text = activeString;
            toggleButton.transform.position -= buttonOffset;
            animator.SetTrigger("In");
        }
    }

    public void DeativateShop()
    {
        buildManager.DeselectTurretToBuild();
        buildManager.DeselectNode();
        shopUI.SetActive(false);
    }

    void ShowStatsUI(TurretBlueprint tb)
    {
        statsUI.SelectTurret(tb);
    }

    public void SetData(int _scoreToUnlockCannon,
        int _scoreToUnlockLaserBeamer,
        int _scoreToUnlockLaser,
        int _scoreToUnlockAntibiotic,
        int _scoreToUnlockMedicine)
    {
        scoreToUnlockCannon = _scoreToUnlockCannon;
        scoreToUnlockLaserBeamer = _scoreToUnlockLaserBeamer;
        scoreToUnlockLaser = _scoreToUnlockLaser;
        scoreToUnlockAntibiotic = _scoreToUnlockAntibiotic;
        scoreToUnlockMedicine = _scoreToUnlockMedicine;
    }
}
