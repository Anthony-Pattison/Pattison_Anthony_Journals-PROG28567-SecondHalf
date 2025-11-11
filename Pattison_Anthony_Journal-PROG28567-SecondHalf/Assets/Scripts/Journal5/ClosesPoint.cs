using UnityEngine;

public class ClosesPoint : MonoBehaviour
{
    public Vector2 closestpoint;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        closestpoint = rb.ClosestPoint(new Vector2(1,1));
    }
}
