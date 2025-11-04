using TMPro;
using UnityEngine;

public class CannonballController : MonoBehaviour
{
    GameObject score;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CannonBall")
        {
            Destroy(gameObject);
            return;
        }
        if (collision.tag == "Target")
        {
            print(collision.name);
            Destroy(gameObject);
            ScoreboardController.Instance.Score++;
        }
    }
}
