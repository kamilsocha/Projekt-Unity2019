using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TMP_Text livesText;

    private void Start()
    {
        PlayerStats playerStats = GameObject.Find("GameMaster").GetComponent<PlayerStats>();
        playerStats.OnPlayerLivesChanged += HandlePlayerLivesChange;
    }

    void HandlePlayerLivesChange()
    {
        livesText.text = PlayerStats.Lives + " LIVES";
    }
}
