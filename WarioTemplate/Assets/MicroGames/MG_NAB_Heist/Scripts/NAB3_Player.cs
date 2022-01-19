using UnityEngine;

public class NAB3_Player : MonoBehaviour, ITickable
{
    public float jumpForce;
    public Animator anim;
    public AudioClip audioClip;

    GameObject gameOver;
    GameObject game;
    GameObject destroyZone;
    GameObject win;

    int tickHolder;

    bool result = true;

    [SerializeField]
    bool isGrounded = false;

    Rigidbody2D RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
 
        AudioManager.Register();

        gameOver = GameObject.Find("GameOver");
        game = GameObject.Find("Game");
        destroyZone = GameObject.Find("DestroyZone");
        win = GameObject.Find("Win");
        gameOver.SetActive(false);
        win.SetActive(false);
        destroyZone.SetActive(false);
    }

    void Start()
    {
        GameManager.Register();
        GameController.Init(this);
        tickHolder = 8;
    }
    
    void Update()
    {
        if (InputManager.GetKeyDown(ControllerKey.A))
        {
            if (isGrounded == true)
            {
                RB.AddForce(Vector2.up * jumpForce);
                isGrounded = false;
                anim.SetBool("isJumping",true);
                PlaySound();
            }
        }
    }
    public void OnTick()
    {
        if (GameController.currentTick == 5)
        {
            GameController.StopTimer();
            game.SetActive(false);
            //result = true; --> inutile
            if (!gameOver.activeSelf)
            {
                destroyZone.SetActive(true);
                win.SetActive(true);
            }
        }

        if (GameController.currentTick == tickHolder)
        {
            GameController.FinishGame(result);
            //Debug.Log("EndofGame");
        }

        Debug.Log(GameController.currentTick);

        /*if (GameController.currentTick == 1)
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
            Debug.Log("8");*/

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            if(isGrounded == false)
            {
                isGrounded = true;
                OnLanding();
            }
        }

        if (collision.gameObject.CompareTag("guard"))
        {
            gameOver.SetActive(true);
            game.SetActive(false);
            destroyZone.SetActive(true);


            result = false;
            tickHolder = GameController.currentTick + 3;
            GameController.StopTimer();
        }
            /*if (destroyZone == null)
                destroyZone.SetActive(true);
            if (!destroyZone.activeSelf)
            {
                destroyZone.SetActive(true);
            }

            result = false;*/

            /*Destroy(collision.gameObject);
            Destroy(this.gameObject);*
        }

        /*if (collision.gameObject.CompareTag("coin"))
        {
            Destroy(collision.gameObject);
        }*/
    }

    public void OnLanding()
    {
        anim.SetBool("isJumping", false);
    }

    private void PlaySound()
    {
        AudioManager.PlaySound(audioClip);
    }
}
