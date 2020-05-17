using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Text livesText;

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
