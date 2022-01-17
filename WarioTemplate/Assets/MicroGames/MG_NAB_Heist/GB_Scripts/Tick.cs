using UnityEngine;
using UnityEngine.SceneManagement;

public class Tick : MonoBehaviour, ITickable
{
    bool result = false;
    GameObject game;
    GameObject win;
    GameObject destroyZone;
    GameObject gameOver;

    /*void Awake()
    {
        game = GameObject.Find("Game");
        destroyZone = GameObject.Find("DestroyZone");
        gameOver = GameObject.Find("gameOver");
    }*/
    void Start()
    {
        GameManager.Register();
        GameController.Init(this);
    }

    public void OnTick()
    {
        if (GameController.currentTick == 1)
            Debug.Log("1");
        if (GameController.currentTick == 2)
            Debug.Log("2");
        if (GameController.currentTick == 3)
            Debug.Log("3");
        if (GameController.currentTick == 4)
            Debug.Log("4");
        if (GameController.currentTick == 5)
            Debug.Log("5");
        if (GameController.currentTick == 6)
            Debug.Log("6");
        if (GameController.currentTick == 7)
            Debug.Log("7");
        if (GameController.currentTick == 8)
            Debug.Log("8");
    }

}