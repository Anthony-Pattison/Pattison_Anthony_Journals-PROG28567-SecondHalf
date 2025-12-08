using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SwingingPlatform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Swinging());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Swinging()
    {
        while (true)
        {
            Vector3 currentRotation = transform.eulerAngles * Mathf.Deg2Rad;
            while (Mathf.Round(currentRotation.z) != 1)
            {
                transform.eulerAngles += new Vector3(0, 0, 20) * Time.deltaTime;
                currentRotation = transform.eulerAngles * Mathf.Deg2Rad;
                yield return null;
            }
            while (Mathf.Round(currentRotation.z) != 5)
            {
                transform.eulerAngles -= new Vector3(0, 0, 20) * Time.deltaTime;
                currentRotation = transform.eulerAngles * Mathf.Deg2Rad;
                yield return null;
            }
        }
    }
}
