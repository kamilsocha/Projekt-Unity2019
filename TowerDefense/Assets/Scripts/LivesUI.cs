using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TMP_Text livesText;

    private void Awake()
    {
        PlayerStats playerStats = GameObject.Find("GameMaster").GetComponent<PlayerStats>();
        playerStats.OnPlayerLivesChanged += HandlePlayerLivesChange;
    }

    void HandlePlayerLivesChange(int lives)
    {
        livesText.text = lives + " LIVES LEFT";
    }
}
