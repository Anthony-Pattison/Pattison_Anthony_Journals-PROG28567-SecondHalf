using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject PrefabBall;
    public int BallSpawnCount = 20;
    public float BallSpawnInterval = 0.3f;
    public float BallForceChange = 0.1f;
    public bool RandomColor = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        for (int i = 0; i<BallSpawnCount; i++)
        {
            GameObject ball =  Instantiate(PrefabBall, transform.position, Quaternion.identity, transform);
            Rigidbody2D body2D = ball.GetComponent<Rigidbody2D>();
            if (body2D != null)
            {
                body2D.AddForce(Random.insideUnitCircle.normalized * BallForceChange, ForceMode2D.Impulse);
            }
            if (RandomColor)
            {
                ball.GetComponent<SpriteRenderer>().color = new (Random.value, Random.value, Random.value);
            }
            yield return new WaitForSeconds(BallSpawnInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
