using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Animator anim; 
    
    public void Start()
    {
        //anim.SetBool("isDead", true);
    }
    
    public void Retry()
    {
        //Debug.Log("Retry Ok !");
        //SceneManager.LoadScene("MG_NAC_Scene_Heist");
    }

    /*public void QuitGame()
    {
        Debug.Log("Quit Game Ok !");
        Application.Quit();
    }*/

}
