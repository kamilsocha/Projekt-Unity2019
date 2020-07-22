using UnityEngine;
using TMPro;

/// <summary>
/// Changes text displaying amount of lives when event informing about it's change occurs.
/// </summary>
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
