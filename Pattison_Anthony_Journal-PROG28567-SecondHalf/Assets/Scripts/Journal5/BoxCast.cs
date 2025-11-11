using UnityEngine;

public class BoxCast : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position,transform.position + Vector3.up * 20);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector3.one, 0, Vector2.up, 20);
        if (hit)
        {
            print(hit);
        }
    }

}
