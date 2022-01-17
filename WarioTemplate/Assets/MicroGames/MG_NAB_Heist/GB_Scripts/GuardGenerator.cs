using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGenerator : MonoBehaviour
{
    public GameObject guard;

    public float MinSpeed;
    public float MaxSpeed;
    public float currentSpeed;

    public float SpeedMultiplier;

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
        GameObject GuardIns = Instantiate(guard, transform.position, transform.rotation);

        GuardIns.GetComponent<Guard>().guardGenerator = this;
    }

    void Update()
    {
        if(currentSpeed < MaxSpeed)
        {
            currentSpeed += SpeedMultiplier;
        }
    }
}
