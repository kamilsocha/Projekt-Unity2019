using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//[System.Serializable]


public class MoneyUI : MonoBehaviour
{
    public Text moneyText;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + PlayerStats.Money;        
    }
}
