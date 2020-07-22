using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls animation of rounds survived by player. Counts up from zero to number
/// of survived rounds. Displayed on level won and game over panels.
/// </summary>
public class RoundsSurvived : MonoBehaviour
{
    public TMP_Text roundsText;
    PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(.7f);
        while(round < playerStats.Rounds)
        {
            round++;
            roundsText.text = round.ToString();
            yield return new WaitForSeconds(.05f);
        }
    }
}
