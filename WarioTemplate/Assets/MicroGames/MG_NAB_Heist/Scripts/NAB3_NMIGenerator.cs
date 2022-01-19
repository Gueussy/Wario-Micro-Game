using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAB3_NMIGenerator : MonoBehaviour
{
    public GameObject enemy;

    public float MinSpeed;
    public float MaxSpeed;
    public float currentSpeed;

    //public float SpeedMultiplier;

    void Awake()
    {
        currentSpeed = MinSpeed;
        generateGuard();
    }

    public void GenerateNextGuardWithGap()
    {
        //float randomWait = Random.Range(0.1f, 1.2f);
        float randomWait = Random.Range(1.2f, 3.7f);
        Invoke("generateGuard", randomWait);
    }

    public void generateGuard()
    {
        GameObject NMIIns = Instantiate(enemy, transform.position, transform.rotation);

        NMIIns.GetComponent<NAB3_NMI>().enemyGenerator = this;
    }

    /*void Update()
    {
        if(currentSpeed < MaxSpeed)
        {
            currentSpeed += SpeedMultiplier;
        }
    }*/
}
