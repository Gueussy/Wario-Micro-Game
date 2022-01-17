using UnityEngine;

public class Guard : MonoBehaviour
{
    public GuardGenerator guardGenerator;
    public AudioClip audioClip;

    void Awake()
    {
        AudioManager.Register();
    }
    
    void Update()
    {
        transform.Translate(Vector2.left * guardGenerator.currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("nextLine"))
        {
            guardGenerator.GenerateNextGuardWithGap();
            PlaySound();
        }

        if (collision.gameObject.CompareTag("finishLine"))
        {
            StopSound(); 
            Destroy(this.gameObject);

        }
    }

    private void PlaySound()
    {
        AudioManager.PlaySound(audioClip);
    }

    private void StopSound()
    {
        AudioManager.StopSound(audioClip);
    }

}
