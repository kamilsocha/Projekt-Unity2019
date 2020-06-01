using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MoneyUI : MonoBehaviour
{
    public TMP_Text moneyText;

    private void Start()
    {
        PlayerStats playerStats = GameObject.Find("GameMaster").GetComponent<PlayerStats>();
        playerStats.OnPlayerMoneyChanged += HandlePlayerMoneyChanged;
    }

    void HandlePlayerMoneyChanged()
    {
        moneyText.text = "$" + PlayerStats.Money;
    }

}
