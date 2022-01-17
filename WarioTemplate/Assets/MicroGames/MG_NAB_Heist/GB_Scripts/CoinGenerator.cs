using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public GameObject coin;

    public float coinMinSpeed;
    public float coinMaxSpeed;
    public float coinCurrentSpeed;

    public float coinSpeedMultiplier;

    void Awake()
    {
        coinCurrentSpeed = coinMinSpeed;
        generateCoin();
    }

    public void GenerateNextCoinWithGap()
    {
        //float randomWait = Random.Range(0.1f, 1.2f);
        float randomWait = Random.Range(1.2f, 3.7f);
        Invoke("generateCoin", randomWait);
    }

    public void generateCoin()
    {
        GameObject CoinIns = Instantiate(coin, transform.position, transform.rotation);

        CoinIns.GetComponent<Coin>().coinGenerator = this;
    }

    void Update()
    {
        if (coinCurrentSpeed < coinMaxSpeed)
        {
            coinCurrentSpeed += coinSpeedMultiplier;
        }
    }
}
