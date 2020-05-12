using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//[System.Serializable]


public class MoneyUI : MonoBehaviour
{
    public Text moneyText;

    private void Start()
    {
        PlayerStats playerStats = GameObject.Find("GameMaster").GetComponent<PlayerStats>();
        playerStats.OnPlayerMoneyChanged += HandlePlayerMoneyChanged;
    }

    void HandlePlayerMoneyChanged()
    {
        moneyText.text = "$" + PlayerStats.Money;
    }


    /*void Update()
    {
        moneyText.text = "$" + PlayerStats.Money;        
    }*/
}
