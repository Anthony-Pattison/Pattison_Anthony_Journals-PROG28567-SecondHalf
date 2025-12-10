using UnityEngine;

public class PlayerMoveWithPlatform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position += new Vector3(0, 0, 20) * Time.deltaTime;
        }
    }
}
