using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinGenerator coinGenerator;

    void Update()
    {
        transform.Translate(Vector2.left * coinGenerator.coinCurrentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("nextLine"))
        {
            coinGenerator.GenerateNextCoinWithGap();
        }

        if (collision.gameObject.CompareTag("finishLine"))
        {
            Destroy(this.gameObject);
        }
    }
}
