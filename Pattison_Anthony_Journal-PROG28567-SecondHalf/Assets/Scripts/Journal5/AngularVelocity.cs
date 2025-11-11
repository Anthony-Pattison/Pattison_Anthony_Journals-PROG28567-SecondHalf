using UnityEngine;
using UnityEngine.UIElements;

public class AngularVelocity : MonoBehaviour
{
    public float angularVelocity = 90;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = angularVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.angularDamping = 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.angularVelocity = angularVelocity;
            rb.angularDamping = 0;
        }
    }
}
