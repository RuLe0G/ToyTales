using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public TMP_Text coinCounterText;
    private int coinCount = 0;

    private void OnEnable()
    {
        Coin.OnCoinCollected += UpdateCoinCounter;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= UpdateCoinCounter;
    }

    private void UpdateCoinCounter(int coinValue)
    {
        coinCount += coinValue;
        coinCounterText.text = coinCount.ToString();
    }
}
