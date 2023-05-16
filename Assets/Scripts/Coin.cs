using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int coinRare = 1;

    public delegate void CoinCollected(int coinValue);
    public static event CoinCollected OnCoinCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            other.GetComponent<CharStats>().addCoin(coinRare);
            OnCoinCollected?.Invoke(coinRare);

            Destroy(gameObject);
        }
    }

}
