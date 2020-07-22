using TMPro;
using UnityEngine;

/// <summary>
/// Manages displaying proper amount of money currently scored by player.
/// </summary>
public class MoneyUI : MonoBehaviour
{
    public TMP_Text moneyText;

    private void Awake()
    {
        PlayerStats playerStats = GameObject.Find("GameMaster").GetComponent<PlayerStats>();
        playerStats.OnPlayerMoneyChanged += HandlePlayerMoneyChanged;
    }

    void HandlePlayerMoneyChanged(int money)
    {
        moneyText.text = "$" + money;
    }

}
