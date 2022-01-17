using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPicker : MonoBehaviour
{
    public Sprite[] carList;
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
        sr.sprite = carList[Random.Range(0, carList.Length)];
    }
    void Update()
    {
        
    }
}
