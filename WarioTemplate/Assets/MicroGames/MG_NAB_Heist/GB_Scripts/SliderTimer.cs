using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SliderTimer : MonoBehaviour
{
    public Slider timerSlider;
    public TMP_Text timerText;
    public float gameTime;
    private float timer = 0.0f;
    
    private bool stopTimer;

    void Start()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float time = gameTime - timer;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time - minutes * 60f);

        string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (time <= 0)
        {
            stopTimer = true;
            SceneManager.LoadScene("MG_NAB_Scene_Heist_YouWin");
        }

        if(stopTimer == false)
        {
            timerText.text = textTime;
            timerSlider.value = time;
        }
    }
}
